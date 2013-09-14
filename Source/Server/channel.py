class Channel:
    def __init__(self, channelName):
        self.name = channelName
        self.subscribers = set()

    def subscribe(self, user):
        self.subscribers.add(user)

    def unsubscribe(self, user):
        self.subscribers.remove(user)

    def broadcast(self, msg):
        pass