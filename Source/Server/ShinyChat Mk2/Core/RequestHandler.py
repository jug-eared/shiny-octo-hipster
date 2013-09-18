import socket
from socketserver import BaseRequestHandler
from threading import Thread

import Core.Message.MessageHandler as MessageHandler
from Core.Message.MessageBuffer import MessageBuffer
from Core.User import User


class RequestHandler(BaseRequestHandler):
    '''Handles connections from the ThreadingTCPServer
    
    setup for initialization
    finish for cleanup
    '''
    def setup(self):
        self._running = True
        
    def handle(self):
        '''Processes incoming messages
        
        starts new daemon-thread for each message
        '''
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
                processMsg = Thread(target=MessageHandler.handle,
                                    args=(msg, newUser), daemon=True)
                processMsg.start()
                pass
        
        newUser.unsubscribe_all()
            
    def finish(self):
        pass  # Add Cleanup
    
    def terminate(self):
        self._running = False