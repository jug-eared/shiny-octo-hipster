import socket
from socketserver import BaseRequestHandler

from Core.MessageBuffer import MessageBuffer


class RequestHandler(BaseRequestHandler):
    def setup(self):
        self._running = True
        
    def handle(self):
        self.request.settimeout(5)
        
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
                print('polled message')
            
    def finish(self):
        pass  # Add Cleanup
    
    def terminate(self):
        self._running = False