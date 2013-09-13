# Server with multiple connections
import socket
import threading
from collections import deque

# Logging Settings
from log import init_log

# for Message, Channel, User
from message import Message, Channel, User



# Logger Object (Settings in log.py)
log = init_log(__name__)



HOST = ''           # listen for any hostname
PORT = 50007

# create IPv4 streaming socket
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((HOST, PORT))
s.listen(5)
# FIXME: Name

running = True

def serve_client(sock):
    # Queue of Message objects
    msgQueue = deque()

    # Buffer for incoming data
    msgBuffer = bytes()

    # required length to process buffer
    reqLength = 0

    while True:
        data = sock.recv(8192)
        msgBuffer += data

        if not data:  # closed socket returns 0 bytes (False) -> exit while loop
            break

    sock.close()

while running:
    # accept connection
    connection, address = s.accept()

    log.debug(str(address[0]) + ' on port ' + str(address[1]))

    # serve client in thread and continue accepting client connections
    serveThread = threading.Thread(target=serve_client, args=(connection,))
    serveThread.start()

s.close()
