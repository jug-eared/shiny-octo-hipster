# Server with multiple connections
import socket
import threading
from collections import deque

# Logging Settings
from log import init_log

# for Message, Channel, User
from message import Message
from user import User



# Logger Object (Settings in log.py)
log = init_log(__name__)


# listen for any hostname
HOST = ''
PORT = 50007

# create IPv4 streaming socket
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((HOST, PORT))
s.listen(5)


running = True

def serve_client(connection, address):
    # Create User
    newUser = User(connection, address)

    # Queue of Message objects
    msgQueue = deque()

    # Buffer for incoming data
    msgBuffer = bytes()

    # Receive loop
    while True:
        data = connection.recv(8192)
        msgBuffer += data

        # msgBuffer processing loop
        while len(msgBuffer) > 0:
            if len(msgBuffer) >= 8:
                # calculate required length to process buffer FIXME: unnecesary calculations
                optionSize = int.from_bytes(msgBuffer[0:4], byteorder='big')
                messageSize = int.from_bytes(msgBuffer[4:8], byteorder='big')
                reqLength = 8 + optionSize + messageSize

                # process buffer or return
                if len(msgBuffer) >= reqLength:
                    # create new Message
                    newMsg = Message()
                    newMsg.from_bytes(msgBuffer[:reqLength])

                    # append Message to Message Queue
                    msgQueue.append(newMsg)

                    # remove bytes from buffer
                    msgBuffer = msgBuffer[reqLength:]
                else:
                    break
            else:
                break

        # handle messages in Queue loop
        while True:
            if len(msgQueue) > 0:
                msg = msgQueue.popleft()

                # start thread to process message
                processMsg = threading.Thread(target=msg.handle, args=(newUser,))
                processMsg.start()
            else:
                break

        if not data:  # closed socket returns 0 bytes (False) -> exit while loop
            break

    connection.close()
    log.debug(str(address[0]) + ' on port ' + str(address[1]) + ' closed')

while running:
    # accept connection
    connection, address = s.accept()

    log.debug(str(address[0]) + ' on port ' + str(address[1]))

    # serve client in thread and continue accepting client connections
    serveThread = threading.Thread(target=serve_client, args=(connection, address))
    serveThread.start()

s.close()
