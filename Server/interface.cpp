#include "interface.h"
#include "lobby.h"
#include <sstream>
#include <sys/socket.h>
#include <queue>
#include <stdio.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>
#include <iostream>

Interface::Interface()
{
    socket_type = AF_INET;
    socket_domain = SOCK_STREAM;
    level = SOL_SOCKET;
    options_name = SO_REUSEADDR | SO_REUSEPORT;
    options_value = 1;
    address_info = new sockaddr_in;
    int options_length = sizeof(options_val);


    serverSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(serverSocket_fd, level, options_name, &options_val, options_length);

    clientSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(clientSocket_fd, level, options_name, &options_val, options_length);
}

Interface::Interface(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal)
{
    socket_type = sockType;
    socket_domain = sockDomain;
    level = lvl;
    options_name = optName;
    options_value = optVal;
    int options_length = sizeof(options_val);

    serverSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(serverSocket_fd, level, options_name, &options_val, options_length);

    clientSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(clientSocket_fd, level, options_name, &options_val, options_length);
}

void Interface::Connect(int, port, unsigned addr)
{
    socket_address_info.sin_family = socket_type;
    socket_address_info.sin_addr = addr;
    socket_address_info.sin_port = htons(port);
    int address_size = sizeof(address_info);

    bind(server_socket_fd, address_info, address_size);
    listen(serverSocket_fd, 100);

}
