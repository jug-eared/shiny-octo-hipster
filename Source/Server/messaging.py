# Represent sent messages / channels / users


# HEAD(32? bytes):
# 2   Type        (Authorization, server command, broadcast to channel,
#                 private msg, etc.)
# 2   source id
# 2   destination id    (private msg / channel)
#
# TBC
# 
# BODY(x bytes):
# x   data


class Message:
    def __init__(self, bytes):
        self._HEAD = bytes[:32]
        self._BODY = bytes[32:]

        self.type = bytes[0:2]
        self.source = bytes[2:4]
        self.destination = bytes[4:6]

    def handle(self):
        # execute command, deliver message, broadcast to channel, etc.
        pass

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