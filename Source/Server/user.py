# User
from message import Message

class User:
    def __init__(self, connection, address, username=''):
        self.connection = connection
        self.address = address

        if username == '':
            self.name = str(address[0]) + ':' + str(address[1])
        else:
            self.name = username

    def send(self, msg):
        data = msg.to_bytes()
        self.connection.sendall(data)
