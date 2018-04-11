#include <vector>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include "network_controller.h"


#pragma region-constructors
//Default constructor.
NetworkController::NetworkController()
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

    CreateListenerSocket();
}

//Argument constructor. Allows for the user specification of socket info/options.
NetworkController::NetworkController(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal, int _port, unsigned _address)
{
    //set socket info/optons variables
    socket_type = sockType;
    socket_domain = sockDomain;
    level = lvl;
    options_name = optName;
    options_value = optVal;
    int options_length = sizeof(options_value);
    address = _address;
    port = _port;

    CreateListenerSocket();
}
#pragma endregion

int NetworkController::CreateListenerSocket()
{
    //create the interface's socket and set its options.
    if(listener_socket = socket(socket_type, socket_domain, 0) == 0)
    {
        //if there was an error creating the socket
        exit(EXIT_FAILURE);
    }

    if (setsockopt(listener_socket, level, options_name, &options_value, options_length))
    {
        //if there was an error setting the socket options
        exit(EXIT_FAILURE);
    }

    //set parameters of the address object
    address_info.sin_family = socket_type;
    address_info.sin_addr.s_addr = address;
    address_info.sin_port = htons(port);
    address_size = sizeof(address_info);

    //bind the socket to the address and start the listening process
    //and create a new socket for the client when it connects.
    if(bind(listener_socket, (struct sockaddr *)&address_info, address_size) < 0)
    {
        //if there was an error binding the socket to the address
        exit(EXIT_FAILURE);
    }

    return listener_socket;
}

void NetworkController::BeginListen()
{
    
}