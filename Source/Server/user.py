# User
from message import Message
from channel import Channel, channelList

class User:
    def __init__(self, connection, address, username=''):
        self.connection = connection
        self.address = address
        self.IP = address[0]
        self.PORT = address[1]

        if username == '':
            self.name = str(address[0]) + ':' + str(address[1])
        else:
            self.name = username

    def send(self, msg):
        data = msg.to_bytes()
        self.connection.sendall(data)

    def unsubscribe_all(self):
        # copy channelList
        tempChannels = dict(channelList)

        while True:
            try:
                chName, ch = tempChannels.popitem()
            except KeyError:
                break
            ch.unsubscribe(self)
