# User
from message import Message

class User:
    def __init__(self, socket_, address, username=''):
        self.socket_ = socket_
        self.address = address

        if username == '':
            self.name = str(address[0]) + ':' + str(address[1])
        else:
            self.name = username

    def send(msg):
        socket_.sendall(msg.to_bytes())
