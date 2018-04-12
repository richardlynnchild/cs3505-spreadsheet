#include "client_controller.h"
#include <string>
#include <cstring>
#include <iostream>
#include <sys/socket.h>
#include <netinet/in.h>
#include <pthread.h>
#include "interface.h"

//Sends a specified message to the client.
void ClientController::Send(int client_socket_id, std::string message)
{
    //convert string message to char[].
    int message_length = message.length() + 1;
    char msg_char[message_length];
    strcpy(msg_char, message.c_str());

    send(client_socket_id, msg_char, message_length, 0);
}

std::string ClientController::ParseMessages(char& message_buffer[])
{
    char end_msg = (char) 3;
    char* start = message_buffer;
    char* end = strchr(message_buffer, end_msg);
    if (end == NULL)
      return NULL;

    int num_bytes = end-start;
    char msg_arr[num_bytes];
    memcpy(&msg_arr, &message_buffer, num_bytes);

    std::string msg_str(msg_arr);

    std::fill(start, end, 0);
    return msg_str;
}

//Clears the memory associated with the given char[].
void ClientController::ClearBuffer(char& buffer[])
{
    char * array_start = buffer;
    char * array_end = array_start + buf_size;
    std::fill(array_start, array_end, 0);
}

//Reads from the incoming buffer and returns any messages sent from the client.
void ClientController::Receive(int client_socket_id, char& message_buffer[])
{
    int bytes_received = recv(client_socket_id, &message_buffer, buf_size, MSG_DONTWAIT);
    if (bytes_received == EAGAIN || bytes_received == EWOULDBLOCK)
    {
      return;
    }
}

void* ClientController::ClientLoop(void* ptr)
{
  Interface* ptr_interface = (Interface*) ptr;
  int client_socket_id = ptr_interface->GetClientID();
  char message_buffer[1024];

  while (true)
  {
     Receive(client_socket, &message_buffer);
     ParseMessages(&message_buffer);
     ClearBuffer(&message_buffer);
  }
}
