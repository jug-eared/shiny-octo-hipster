import socket
from socketserver import BaseRequestHandler
from threading import Thread

from Core.Message.MessageBuffer import MessageBuffer
from Core.User import User


class RequestHandler(BaseRequestHandler):
    def setup(self):
        self._running = True
        
    def handle(self):
        self.request.settimeout(5)
        
        # Create User        
        newUser = User(self.request, self.client_address)
        
        # Create Buffer        
        msgBuffer = MessageBuffer()
        
        while self._running:
            try:
                data = self.request.recv(8192)
            except socket.timeout:
                print('timeout')
                continue
            except ConnectionResetError:
                print('connection reset')
                break
            
            if not data: break
                        
            msgBuffer.append(data)
            msg = msgBuffer.poll()
            
            if msg != None:
                processMsg = Thread(target=msg.handle, args=(newUser,), daemon=True)
                processMsg.start()
        
        newUser.unsubscribe_all()
            
    def finish(self):
        pass  # Add Cleanup
    
    def terminate(self):
        self._running = False