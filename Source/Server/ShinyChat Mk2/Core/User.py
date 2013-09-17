class User:
    _userList = set()
    
    def __init__(self, connection, address):
        self.connection = connection
        self.address = address
        self.IP = address[0]
        self.PORT = address[1]
        
        self.name = str(self.IP) + ':' + str(self.PORT)
        
        User._userList.append(self)
    
    def send(self, message):
        data = message.to_bytes()
        self.connection.sendall(data)