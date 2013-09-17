class Message:
    def __init__(self, options='', message=''):
        self.options = options
        self.message = message
        
    def from_bytes(self, bytes_):
        # reset
        self.options = ''
        self.message = ''
        
        # calculate block sizes     
        optionSize = int.from_bytes(bytes_[0:4], byteorder='big')
        messageSize = int.from_bytes(bytes_[4:8], byteorder='big')
        
        # calculate start and end-bytes
        optionStart, optionEnd = 8, 8 + optionSize
        messageStart, messageEnd = optionEnd, optionEnd + messageSize
        
        # get byte-blocks
        options = bytes_[optionStart:optionEnd]
        message = bytes_[messageStart:messageEnd]
        
        # decode byte-blocks
        self.options = options.decode(encoding='utf-8')
        self.message = message.decode(encoding='utf-8')