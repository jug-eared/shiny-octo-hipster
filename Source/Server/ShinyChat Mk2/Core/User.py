class User:
    _userList = set()
    
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
        data = message.to_bytes()
        self.connection.sendall(data)
        
    def unsubscribe_all(self):
        for channel in self.subscriptions:
            channel.unsubscribe(self)
