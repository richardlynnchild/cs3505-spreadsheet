#include <iostream>
#include "interface.h"
#include <stdio.h>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>
#include <arpa/inet.h>

using namespace std;

class SampleClient
{
private:
    const static int buf_size = 1024;
    char buffer[buf_size];
    int clientSocket_fd;
    int socket_domain, socket_type;
    struct sockaddr_in server_address, client_address;

    char * buffer_start = buffer;
    char * buffer_end = buffer_start + buf_size;

    void ClearBuffer()
    {
        std::fill(buffer_start, buffer_end, 0);
    }

public:
    SampleClient()
    {
        //set socket info/optons variables
        socket_type = AF_INET;
        socket_domain = SOCK_STREAM;

        //create the client's socket
        clientSocket_fd = socket(socket_type, socket_domain, 0);
        memset(&server_address, '0', sizeof(server_address));
    }

    void Connect(int port, std::string address)
    {
        //set server address info
        server_address.sin_family = socket_type;
        server_address.sin_port = htons(port);

        //convert string address to char[].
        int address_length = address.length() + 1;
        char addr_char[address_length];
        strcpy(addr_char, address.c_str());

        inet_pton(socket_type, addr_char, &server_address.sin_addr);

        connect(clientSocket_fd, (struct sockaddr *)&server_address, sizeof(server_address));
    }

    void Send(std::string message)
    {
        //convert string message to char[].
        int message_length = message.length() + 1;
        char msg_char[message_length];
        strcpy(msg_char, message.c_str());

        send(clientSocket_fd, msg_char, message_length, 0);
    }

    std::string Receive()
    {
        int bytes_recieved = recv(clientSocket_fd, buffer, buf_size, 0);
        std::string msg(buffer);
        ClearBuffer();
        return msg;
    }

};

int main()
{
    cout << "Main.cpp" << endl;
    SampleClient client;
    Interface server;
    server.Connect();
    client.Connect(2112, "127.0.0.1");
    server.Send("hello");
    cout << client.Receive() << endl;
}