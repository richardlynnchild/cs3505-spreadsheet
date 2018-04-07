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
#include <pthread.h>

//Default constructor
Interface::Interface()
{
    //set socket info/optons variables
    socket_type = AF_INET;
    socket_domain = SOCK_STREAM;
    level = SOL_SOCKET;
    options_name = SO_REUSEADDR | SO_REUSEPORT;
    options_value = 1;
    int options_length = sizeof(options_value);
    address = INADDR_ANY;
    port = 2112;

    //create the interface's socket and set its options.
    interfaceSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(interfaceSocket_fd, level, options_name, &options_value, options_length);
}

//Argument constructor. Allows for the user specification of socket info/options.
Interface::Interface(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal, int _port, unsigned _address)
{
    //set socket info/optons variables
    socket_type = sockType;
    socket_domain = sockDomain;
    level = lvl;
    options_name = optName;
    options_value = optVal;
    int options_length = sizeof(options_value);
    port = _port;
    address = _address;

    //create the interface's socket and set its options.
    interfaceSocket_fd = socket(socket_type, socket_domain, 0);
    setsockopt(interfaceSocket_fd, level, options_name, &options_value, options_length);
}

//Binds the interface socket to the specified address and port, then listens for a client connection
//and creates a client socket for it.
void * Interface::ConnectHelper()
{

    //set parameters of the address object
    address_info.sin_family = socket_type;
    address_info.sin_addr.s_addr = address;
    address_info.sin_port = htons(port);
    address_size = sizeof(address_info);

    //bind the socket to the address and start the listening process
    //and create a new socket for the client when it connects.
    bind(interfaceSocket_fd, (struct sockaddr *)&address_info, address_size);
    listen(interfaceSocket_fd, 1); // currently only allows for one client to connect to this socket
    clientSocket_fd = accept(interfaceSocket_fd, (struct sockaddr *)&address_info, (socklen_t*)&address_size);
}

void Interface::Connect()
{
    pthread_create(&connection_listener, NULL, ConSub_helper, this);
}

void * Interface::ConSub_helper(void * ptr)
{
    return ((Interface *)ptr)->ConnectHelper();
}

//Reads from the incoming buffer and returns any messages sent from the client.
std::string Interface::Receive()
{
    int bytes_recieved = recv(clientSocket_fd, incoming_buffer, buf_size, 0);
    std::string msg(incoming_buffer);
    ClearBuffer(incoming_buffer);
    return msg;
}

//Sends a specified message to the client.
void Interface::Send(std::string message)
{
    //convert string message to char[].
    int message_length = message.length() + 1;
    char msg_char[message_length];
    strcpy(msg_char, message.c_str());

    send(clientSocket_fd, msg_char, message_length, 0);
}

//Clears the memory associated with the given char[].
void Interface::ClearBuffer(char buffer[])
{
    char * array_start = buffer;
    char * array_end = array_start + buf_size;
    std::fill(array_start, array_end, 0);
}



/*******************
***REMOVE SECTION***
*******************/

//a test function for testing things out -- to be removed.
std::string Interface::test()
{
    std::string msg(incoming_buffer);
    return msg;
}
