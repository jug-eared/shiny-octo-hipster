import threading

channelList = dict()
channelNames = list()

class Channel:
    def __init__(self, channelName):
        self.name = channelName
        self.subscribers = set()

        global channelList

        if self.name in channelList:
            raise RuntimeError('Channel already exists')

        channelList[self.name] = self
        channelNames.append(self.name)

    def subscribe(self, user):
        self.subscribers.add(user)

    def unsubscribe(self, user):
        if user in self.subscribers:
            self.subscribers.remove(user)

    def get_subscribers(self):
        return self.subscribers

    def broadcast(self, msg):
        for user in self.subscribers:
            # FIXME: Check if socket is alive
            sendThread = threading.Thread(target=user.send, args=(msg,))
            sendThread.start()