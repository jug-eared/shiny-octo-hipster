# Represent sent messages / channels / users
from log import init_log
import xml.etree.ElementTree as etree
from channel import Channel, channelList, channelNames
from xml_template import xml_options, xml_message

# Logging settings
log = init_log(__name__)


MESSAGE_TYPE = {
    'UNDEFINED': '0',
    'MESSAGE': '1',
    'COMMAND': '2',
    'RESPONSE': '3'
}

COMMAND_TYPE = {
    'UNDEFINED': '0',
    'JOIN_CHANNEL': '1',
    'LEAVE_CHANNEL': '2',
    'GET_CHANNELS': '3',
    'GET_USER': '4',
    'USER_JOINS': '5',
    'USER_LEAVES': '6'
}


class Message:
    def __init__(self, options='', message=''):
        self.options = options
        self.message = message
        self.optionTags = dict()
        self.messageTags = dict()

        self.parse_tags()

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

        # Parse Options, Message XML
        self.parse_tags()

    def parse_tags(self):
        # Parse Options
        if self.options != '':
            root = etree.fromstring(self.options)
            self.optionTags = {child.tag: child.text for child in root}

        # Parse Message
        if self.message != '':
            root = etree.fromstring(self.message)
            self.messageTags = {child.tag: child.text for child in root}        


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

    def handle(self, user):
        # execute command, deliver message, broadcast to channel, etc.
        # log.debug(self.options)
        # log.debug(self.message)

        msgType = self.optionTags['messageType']

        if msgType == MESSAGE_TYPE['UNDEFINED']:
            self.handle_undefined(user)
        elif msgType == MESSAGE_TYPE['MESSAGE']:
            self.handle_message(user)
        elif msgType == MESSAGE_TYPE['COMMAND']:
            self.handle_command(user)
        elif msgType == MESSAGE_TYPE['RESPONSE']:
            self.handle_response(user)
        else:
            log.warning('Message type unknown')


    # Message Handler

    def handle_undefined(self, user):
        log.debug('received Message-Type undefined')
        pass

    def handle_message(self, user):
        msgChannel = self.optionTags['channel']
        channelList[msgChannel].broadcast(self)

    def handle_command(self, user):
        command = self.optionTags['command']

        if command == COMMAND_TYPE['UNDEFINED']:
            self.command_undefined(user)
        elif command == COMMAND_TYPE['JOIN_CHANNEL']:
            self.command_join_channel(user)
        elif command == COMMAND_TYPE['LEAVE_CHANNEL']:
            self.command_leave_channel(user)
        elif command == COMMAND_TYPE['GET_CHANNELS']:
            self.command_get_channels(user)
        elif command == COMMAND_TYPE['GET_USER']:
            self.command_get_users(user)
        elif command == COMMAND_TYPE['USER_JOINS']:
            self.command_user_joins(user)
        elif command == COMMAND_TYPE['USER_LEAVES']:
            self.command_user_leaves(user)
        else:
            log.warning('unknown command')

    def handle_response(self, user):
        log.debug('response')
        pass


    # COMMANDS

    def command_undefined(self, user):
        log.debug('received Command undefined')
        pass

    def command_join_channel(self, user):
        channelRequest = self.optionTags['channel']

        if channelRequest not in channelList:
            log.debug('creating channel: ' + channelRequest)
            Channel(channelRequest)
            channelList[channelRequest].subscribe(user)

        channelList[channelRequest].subscribe(user)
        log.debug(str(user.IP) + ' subscribed to channel ' + channelRequest)


    def command_leave_channel(self, user):
        channelRequest = self.optionTags['channel']

        if user in channelList[channelRequest].get_subscribers():
            channelList[channelRequest].unsubscribe(user)
        else:
            pass

    # Test required
    def command_get_channels(self, user):
        chNameString = ';'.join(channelNames)

        optionDict = dict(
            id=self.optionTags['id'],
            messageType=MESSAGE_TYPE['RESPONSE'],
            command='',
            channel='',
            user=''
            )

        log.debug(xml_options(optionDict))
        log.debug(xml_message(chNameString))

        newMsg = Message(xml_options(optionDict), xml_message(chNameString))
        user.send(newMsg)

    # Test required
    def command_get_users(self, user):
        userNames = (user.name for user in channelList[self.optionTags['channel']])
        userNameString = ';'.joins(userNames)

        optionDict = dict(
            id=self.optionTags['id'],
            messageType=MESSAGE_TYPE['RESPONSE'],
            command='',
            channel=self.optionTags['channel'],
            user=''
            )

        log.debug(xml_options(optionDict))
        log.debug(xml_message(userNameString))

        newMsg = Message(xml_options(optionDict), xml_message(userNameString))
        user.send(newMsg)
