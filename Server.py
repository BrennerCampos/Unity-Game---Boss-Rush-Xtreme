import socket
import json
import PlayerState
import datetime
import arcade
import random
from typing import Dict

SERVER_PORT = 25001

def find_ip_address():
    server_address = ""
    connection = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    try:
        connection.connect(('10.255.255.255', 1))   # dummy address that doesn't need to be reached
        server_address = connection.getsockname()[0]
    except IOError:
        server_address = '127.0.0.1'    # localhost loopback address
    finally:
        connection.close()
    return server_address

def main():
    server_address = find_ip_address()
    print(f"Server address is {server_address} on port {SERVER_PORT}")

    # create a socket and bind it to the address and port
    UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
    UDPServerSocket.bind((server_address, SERVER_PORT))

    while(True):
        data_packet = UDPServerSocket.recvfrom(1024)    # sets the packet size
        message = data_packet[0]            # data stored here within tuple
        client_address = data_packet[1]     # client IP addr is stored here, nothing beyond [1]
        # print(f"from: {client_address}")
        # print(f"\tmessage: {message}")

        # do something here if first time seeing player

        UDPServerSocket.sendto(str.encode("server recieved your message"), client_address)

if __name__ == '__main__':
    main()