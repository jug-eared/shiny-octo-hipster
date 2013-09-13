# Server with multiple connections
import socket
import threading

# Logging Settings
from log import init_log

# for Message, Channel, User
from messaging import Message, Channel, User



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
    # only for testing purpose
    log.debug('serve_client thread started:')
    message = bytes()
    while True:
        data = sock.recv(1024)
        message += data

        if not data:  # closed socket returns 0 bytes (False) -> exit while loop
            break

    sock.close()
    log.debug('Received data: ' + message.decode(encoding='utf-8'))



while running:
    # accept connection
    connection, address = s.accept()

    log.debug(str(address[0]) + ' on port ' + str(address[1]))

    # serve client in thread and continue accepting client connections
    fred = threading.Thread(target=serve_client, args=(connection,))
    fred.start()
    #FIXME: Name

s.close()
