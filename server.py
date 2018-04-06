import socket

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind(('192.168.1.15', 5555 ))
print('Server > 192.168.1.15:5555')
clients = []

server_socket.listen(10)

while True:
    client_socket, addr = server_socket.accept()

    if addr not in clients:
        clients.append(addr)
    
    while True:
        data = client_socket.recv(4096)
        if not data:
            break
        print('Collecting data... from pidors on client')
        

    # Обработка информации
    print(data.decode('utf-8'))

    for client in clients:
        server_socket.sendto(data, client)

server_socket.close()
