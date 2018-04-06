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

//Default constructor.
Interface::Interface()
{
    //set socket info/optons variables
    socket_type = AF_INET;
    socket_domain = SOCK_STREAM;
    level = SOL_SOCKET;
    options_name = SO_REUSEADDR | SO_REUSEPORT;
    options_value = 1;
    address_info = new sockaddr_in;
    int options_length = sizeof(options_val);

    //create the interface's socket and set its options.
    interfaceSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(interfaceSocket_fd, level, options_name, &options_val, options_length);
}

//Argument constructor. Allows for the user specification of socket info/options.
Interface::Interface(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal)
{
    //set socket info/optons variables
    socket_type = sockType;
    socket_domain = sockDomain;
    level = lvl;
    options_name = optName;
    options_value = optVal;
    int options_length = sizeof(options_val);

    //create the interface's socket and set its options.
    interfaceSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(interfaceSocket_fd, level, options_name, &options_val, options_length);
}

//Binds the interface socket to the specified address and port, then listens for a client connection
//and creates a client socket for it.
void Interface::Connect(int, port, unsigned address)
{
    //set parameters of the address object
    *address_info.sin_family = socket_type;
    *address_info.sin_addr = address;
    *address_info.sin_port = htons(port);
    int address_size = sizeof(address_info);

    //bind the socket to the address and start the listening process
    //and create a new socket for the client when it connects.
    bind(interfaceSocket_fd, address_info, address_size);
    listen(interfaceSocket_fd, 1); // currently only allows for one client to connect to this socket
    clientSocket_fd = socket(accept(interfaceSocket_fd, address_info, address_size));
}

//Reads from the incoming buffer and returns any messages sent from the client.
std::string Interface::Receive()
{
    int valread = readv(clientSocket_fd, incoming_buffer, buf_size);

}

//sends a specified message to the client.
void Interface::Send(std::string message)
{

}
