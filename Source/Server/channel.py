import threading

channelList = dict()

class Channel:
    def __init__(self, channelName):
        self.name = channelName
        self.subscribers = set()

        global channelList

        if self.name in channelList:
            raise RuntimeError('Channel already exists')

        channelList[self.name] = self

    def subscribe(self, user):
        self.subscribers.add(user)

    def unsubscribe(self, user):
        self.subscribers.remove(user)

    def broadcast(self, msg):
        for user in self.subscribers:
            sendThread = threading.Thread(target=user.send, args=(msg,))
            sendThread.start()