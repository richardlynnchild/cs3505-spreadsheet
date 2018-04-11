#include <vector>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include "interface_temp.h"

#ifndef NETWORK_CONTROLLER
#define NETWORK_CONTROLLER

class NetworkController
{
private:
    //socket info/options variables
    int socket_domain, level, options_name, options_value, socket_type, address_size;
    int listener_socket;
    unsigned address;
    int port;
    sockaddr_in address_info;

    NetworkController();

    NetworkController(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal, int _port, unsigned _address);

    int CreateListenerSocket();
    
    void BeginListen();

    friend class Interface;

public:

    std::vector<Interface>;

}

#endif