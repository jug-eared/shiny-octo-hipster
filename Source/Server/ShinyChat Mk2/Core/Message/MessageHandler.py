from Core.Constants.MessageType import MessageType
from Core.Constants.Command import Command
from Core.Message.Message import Message
from Core.Channel import Channel
from Core.Utility.XMLHelper import xml_options, xml_message


# Entry point for MessageHandler
def handle(message, user):
    msgType = message.optionTags['messageType']
    
    if msgType == MessageType.UNDEFINED:
        handle_undefined(message, user)
    elif msgType == MessageType.MESSAGE:
        handle_message(message, user)
    elif msgType == MessageType.COMMAND:
        handle_command(message, user)
    elif msgType == MessageType.RESPONSE:
        handle_response(message, user)
    else:
        pass


# Handlers for different message-types    
def handle_undefined(message, user):
    pass

def handle_message(message, user):
    msgChannel = message.optionTags['channel']
    print(message.messageText)
    Channel._channelList[msgChannel].broadcast(message)
    
def handle_command(message, user):
    cmd = message.optionTags['command']
    
    if cmd == Command.UNDEFINED:
        cmd_undefined(message, user)
    elif cmd == Command.JOIN_CHANNEL:
        cmd_join_channel(message, user)
    elif cmd == Command.LEAVE_CHANNEL:
        cmd_leave_channel(message, user)
    elif cmd == Command.GET_CHANNELS:
        cmd_get_channels(message, user)
    elif cmd == Command.GET_USER:
        cmd_get_user(message, user)
    elif cmd == Command.USER_JOINS:
        cmd_user_joins(message, user)
    elif cmd == Command.USER_LEAVES:
        cmd_user_leaves(message, user)
    else:
        pass

def handle_response(message, user):
    pass


#Handlers for commands
def cmd_undefined(message, user):
    pass

def cmd_join_channel(message, user):
    channelName = message.optionTags['channel']
    
    if channelName not in Channel._channelList:
        newChannel = Channel(channelName)
    
    newChannel.subscribe(user)        

def cmd_leave_channel(message, user):
    channelName = message.optionTags['channel']
    channel = Channel._channelList[channelName]
    
    if user in channel.subscibers:
        channel.unsubscribe(user)
    else:
        pass

def cmd_get_channels(message, user):
    nameString = ';'.join(Channel.name_list())
    
    optionMap = dict(identifier = message.optionTags['id'],
                     messageType = MessageType.RESPONSE,
                     command = '',
                     channel = '',
                     user = '')
    
    messageMap = dict(message = nameString)
    
    newMsg = Message(xml_options(optionMap), xml_message(messageMap))
    user.send(newMsg)

def cmd_get_user(message, user):
    if message.optionTags['channel'] in Channel._channelList:
        channel = Channel._channelList[message.optionTags['channel']]
        nameString = ';'.join(channel.get_subscribers())
    else:
        nameString = ''
        
    optionMap = dict(identifier = message.optionTags['id'],
                     messageType = MessageType.RESPONSE,
                     command = '',
                     channel = message.optionTags['channel'],
                     user = '')
    
    messageMap = dict(message = nameString)
    
    newMsg = Message(xml_options(optionMap), xml_message(messageMap))
    user.send(newMsg)

def cmd_user_joins(message, user):
    pass

def cmd_user_leaves(message, user):
    pass