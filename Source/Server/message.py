# Represent sent messages / channels / users
from log import init_log
import xml.etree.ElementTree as etree

# Logging settings
log = init_log(__name__)

class Message:
    def __init__(self, options='', message=''):
        self.options = options
        self.message = message
        self.optionTags = dict()

    def from_bytes(self, bytes_):
        # Reset
        self.options = ''
        self.message = ''

        # get sizes for option and message block
        optionSize = int.from_bytes(bytes_[0:4], byteorder='big')
        messageSize = int.from_bytes(bytes_[4:8], byteorder='big')

        # determine startbyte / endbyte
        optionStart, optionEnd = 8, 8 + optionSize
        messageStart, messageEnd = 8 + optionSize, 8 + optionSize + messageSize

        # get blocks
        options = bytes_[optionStart:optionEnd]
        message = bytes_[messageStart:messageEnd]

        # decode blocks
        self.options = options.decode(encoding='utf-8')
        self.message = message.decode(encoding='utf-8')


        # Parse XML Options
        root = etree.fromstring(self.options)
        self.optionTags = {child.tag: child.text for child in root}

    def to_bytes(self):
        # encode blocks
        optionsEncode = self.options.encode(encoding='utf-8')
        messageEncode = self.message.encode(encoding='utf-8')

        # build bytes object 
        retBytes = bytes()
        retBytes += len(optionsEncode).to_bytes(4, byteorder='big')
        retBytes += len(messageEncode).to_bytes(4, byteorder='big')
        retBytes += optionsEncode
        retBytes += messageEncode

        return retBytes

    def handle(self):
        # execute command, deliver message, broadcast to channel, etc.
        log.debug(self.options)
        log.debug(self.message)
