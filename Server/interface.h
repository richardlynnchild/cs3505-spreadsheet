#include "lobby.h"
#include <sstream>
#include <sys/socket.h>
#include <queue>
#include <stdio.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>
#include <iostream>

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
    int socket_domain, level, options_name, options_value;
    int socket_type;

    //connection address struct
    sockaddr_in address_info;

    //methods
    void AcceptConnection();

public:
    //methods
    std::string Receive();
    void Send(std::string);
    void Connect(int port, unsigned address);

    //constructors
    Interface();
    Interface(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal);

};
