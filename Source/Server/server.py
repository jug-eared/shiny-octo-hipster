# Server with multiple connections
import socket
import threading
# for Message, Channel, User
from messaging import Message, Channel, User

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
    print('serve_client thread started:')
    message = bytes()
    while True:
        data = sock.recv(1024)
        message += data

        if not data:  # closed socket returns 0 bytes (False) -> exit while loop
            break

    sock.close()
    print('Received data: ' + message.decode(encoding='utf-8'))



while running:
    # accept connection
    connection, address = s.accept()

    print(str(address[0]) + ' on port ' + str(address[1]))

    # serve client in thread and continue accepting client connections
    fred = threading.Thread(target=serve_client, args=(connection,))
    fred.start()
    #FIXME: Name

s.close()