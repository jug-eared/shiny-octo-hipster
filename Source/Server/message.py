# Represent sent messages / channels / users
from log import init_log

# Logging settings
log = init_log(__name__)

class Message:
    def __init__(self, options='', message=''):
        self.options = options
        self.message = message

    def from_bytes(self, bytes):
        # Reset
        self.options = ''
        self.message = ''

        # get sizes for option and message block
        optionSize = int.from_bytes(bytes[0:4], byteorder='big')
        messageSize = int.from_bytes(bytes[4:8], byteorder='big')

        # determine startbyte / endbyte
        optionStart, optionEnd = 8, 8 + optionSize
        messageStart, messageEnd = 8 + optionSize, 8 + optionSize + messageSize

        # get blocks
        options = bytes[optionStart:optionEnd]
        message = bytes[messageStart:messageEnd]

        # decode blocks
        self.options = options.decode(encoding='utf-8')
        self.message = message.decode(encoding='utf-8')

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

class Channel:
    def __init__(self):
        pass

    def subscribe(self, user):
        pass

    def unsubscribe(self, user):
        pass

    def broadcast(self, msg):
        pass

class User:
    def __init__(self):
        pass
