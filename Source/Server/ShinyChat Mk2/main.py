from socketserver import ThreadingTCPServer
from Core.RequestHandler import RequestHandler

if __name__ == '__main__':
    HOST, PORT = '', 50007
    
    serv = ThreadingTCPServer((HOST, PORT), RequestHandler)
    serv.serve_forever()