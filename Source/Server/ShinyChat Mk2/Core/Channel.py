from threading import Thread


class Channel:
    _channelList = dict()
    
    @staticmethod
    def name_list():
        return [key for key, _ in Channel._channelList]
    

    def __init__(self, channelName):#
        self.name = channelName
        self.subscribers = set()
        
        if self.name in Channel._channelList:
            raise RuntimeError('Channel already exists')
        else:
            Channel._channelList[self.name] = self
    
    def subscribe(self, user):
        if user not in self.subscribers:
            self.subscribers.add(user)
            user.subscriptions.add(self)
    
    def unsubscribe(self, user):
        if user in self.subscribers:
            self.subscribers.remove(user)
            user.subscriptions.remove(self)
    
    def get_subscribers(self):
        return self.subscribers
    
    def broadcast(self, message):
        for user in self.subscribers:
            # FIXME: Chef if socket is alive / cleanup if it's not
            sendThread = Thread(target=user.send, args=(message,))
            sendThread.start()
