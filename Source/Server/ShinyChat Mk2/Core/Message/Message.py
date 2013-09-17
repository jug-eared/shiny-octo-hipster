from Core.Message.MessageHandler import MessageHandler
from Core.Utility.XMLHelper import parse

class Message(MessageHandler):
    def __init__(self, options='', message=''):
        self.options = options
        self.message = message
        self.optionTags = dict()
        self.messageTags = dict()
        
        self.parse_tags()
        
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
        
        # Parse Options, Message
        self.parse_tags()
        
    def to_bytes(self):
        # encode utf-8 binary data
        optionsEncoded = self.options.encode(encoding='utf-8')
        messageEncoded = self.message.encode(encoding='utf-8')
        
        # build bytes object
        retBytes = bytes()
        retBytes += len(optionsEncoded).to_bytes(4, byteorder='big')
        retBytes += len(messageEncoded).to_bytes(4, byteorder='big')
        retBytes += optionsEncoded
        retBytes += messageEncoded
        
        return retBytes
    
    def parse_tags(self):
        if self.options != '':
            self.optionTags = parse(self.options)
        
        if self.message != '':
            self.messageTags = parse(self.message)
