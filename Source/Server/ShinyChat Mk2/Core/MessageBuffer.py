from queue import Queue
from Core.Message import Message

class MessageBuffer:
    '''Buffer for Shiny-Chat Messages
    
    append(bytes_) takes a byte object and adds it to the Byte-Buffer
    poll() returns Message object or None if the Message-Queue is empty
    '''
    def __init__(self):
        self._byteBuffer = bytes()
        self._msgQueue = Queue()
        
        self._processing_bytes = False
        self._required_length = None
        
    def append(self, bytes_):
        '''Takes byte object and adds it to the buffer
        
        if the buffer contains a complete message it's added to the Message-Queue
        '''
        self._byteBuffer += bytes_
        
        # Calculates required buffer-size only once per message
        if not self._processing_bytes and len(self._byteBuffer) >= 8:
            optionSize = int.from_bytes(self._byteBuffer[0:4], byteorder='big')
            messageSize = int.from_bytes(self._byteBuffer[4:8], byteorder='big')
            self._required_length = 8 + optionSize + messageSize
            
            self._processing_bytes = True
            
        # Adds complete message to queue
        if len(self._byteBuffer) >= self._required_length and self._processing_bytes:
            newMsg = Message()
            newMsg.from_bytes(self._byteBuffer[:self._required_length])
            
            self._msgQueue.put(newMsg, block=False)
            
            self._byteBuffer = self._byteBuffer[self._required_length:]            
            
            self._processing_bytes = False
            self._required_length = None
            
    def poll(self):
        '''Returns Message object from the Message-Queue or None
        '''
        if not self._msgQueue.empty():
            return self._msgQueue.get(block=False)
        else:
            return None