class User:
    '''Holds users connection info
    
    name_list(): returns a list of all connected users
    '''
    _userList = set()

    @staticmethod
    def name_list():
        return (user.name for user in User._userList)
    
    def __init__(self, connection, address):
        # Connection INFO
        self.connection = connection
        self.address = address
        self.IP = address[0]
        self.PORT = address[1]
        self.name = str(self.IP) + ':' + str(self.PORT)
        
        User._userList.add(self)
        self.subscriptions = set()

    def send(self, message):
        '''Sends message to the user
        '''
        data = message.to_bytes()
        self.connection.sendall(data)
        
    def unsubscribe_all(self):
        '''Unsubscribes the user from all channels
        '''
        for channel in set(self.subscriptions):
            channel.unsubscribe(self)
