from Core.Constants.MessageType import MessageType
from Core.Constants.Command import Command

class MessageHandler:
    def handle(self, user):
        msgType = self.optionTags['messageType']
        
        if msgType == MessageType.UNDEFINED:
            self.handle_undefined(user)
        elif msgType == MessageType.MESSAGE:
            self.handle_message(user)
        elif msgType == MessageType.COMMAND:
            self.handle_command(user)
        elif msgType == MessageType.RESPONSE:
            self.handle_response(user)
        else:
            pass
    
    
    # Handlers for different message-types    
    def handle_undefined(self, user):
        pass
    
    def handle_message(self, user):
        pass
    
    def handle_command(self, user):
        cmd = self.optionTags['command']
        
        if cmd == Command.UNDEFINED:
            self.cmd_undefined(user)
        elif cmd == Command.JOIN_CHANNEL:
            self.cmd_join_channel(user)
        elif cmd == Command.LEAVE_CHANNEL:
            self.cmd_leave_channel(user)
        elif cmd == Command.GET_CHANNELS:
            self.cmd_get_channels(user)
        elif cmd == Command.GET_USER:
            self.cmd_get_user(user)
        elif cmd == Command.USER_JOINS:
            self.cmd_user_joins(user)
        elif cmd == Command.USER_LEAVES:
            self.cmd_user_leaves(user)
        else:
            pass
    
    def handle_response(self, user):
        pass
    
    
    #Handlers for commands
    def cmd_undefined(self, user):
        pass
    
    def cmd_join_channel(self, user):
        pass
    
    def cmd_leave_channel(self, user):
        pass
    
    def cmd_get_channels(self, user):
        pass
    
    def cmd_get_user(self, user):
        pass
    
    def cmd_user_joins(self, user):
        pass
    
    def cmd_user_leaves(self, user):
        pass