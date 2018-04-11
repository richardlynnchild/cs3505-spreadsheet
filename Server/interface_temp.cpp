#include "interface_temp.h"


Interface::Interface(int socket_id)
{
    interfaceSocket_id = socket_id;
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

//Reads from the incoming buffer and returns any messages sent from the client.
void Interface::Receive()
{
    int bytes_recieved = recv(clientSocket_fd, message_buffer, buf_size, 0);
    std::string msg(message_buffer);
    ClearBuffer(message_buffer);
    messages << msg;
}

//
std::string Interface::GetMessage()
{
    Receive();
    outbound_messages.Push(messages.getline());
}

std::string Interface::GetSprdName()
{
    return this->spreadsheet_name;
}