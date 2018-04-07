#include "lobby.h"
#include <sstream>
#include <sys/socket.h>
#include <queue>
#include <stdio.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>
#include <iostream>

#ifndef INTERFACE_H
#define INTERFACE_H

class Interface
{
private:
    //incoming and outgoing message buffers. Initialized as zeroes.
    static const int buf_size = 1024;
    char incoming_buffer[buf_size];
    char outgoing_buffer[buf_size];

    //interface and client networking sockets
    int interfaceSocket_fd, clientSocket_fd;

    //socket info/options variables
    int socket_domain, level, options_name, options_value, socket_type, address_size;

    //connection address struct
    sockaddr_in address_info;
    int port;
    unsigned address;

    //threading
    pthread_t connection_listener;

    //methods
    void ClearBuffer(char buffer[]);
    void * ConnectHelper();
    static void * ConSub_helper(void * ptr);

public:
    //methods
    std::string Receive();
    void Send(std::string);
    void Connect();
    std::string test();

    //constructors
    Interface();
    Interface(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal, int _port, unsigned _address);

};

#endif