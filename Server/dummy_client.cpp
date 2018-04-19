#include <iostream>
#include <string>
#include <cstring>
#include <sys/socket.h>
#include <netinet/in.h>
#include <errno.h>
#include <stdlib.h>

int main(int argc, char* argv[])
{
  
  int sock_descriptor = socket(AF_INET, SOCK_STREAM, 0);
  struct sockaddr_in address;
  address.sin_family = AF_INET;
  address.sin_addr.s_addr = INADDR_ANY;
  address.sin_port = htons(2112);
  socklen_t address_len = sizeof(address);

  if (connect(sock_descriptor, (struct sockaddr*) &address, address_len) != 0)
  {
    std::cout << "Cannot connect!" << std::endl;
    return 0;
  }
  else
  {
    std::cout << "Connected" << std::endl;
  }

  char ext = (char) 3;
  char buffer[9] = "register";
  buffer[8] = ext;

 if ( send(sock_descriptor, buffer, 9, 0) < 0)
    std::cout << strerror(errno) << std::endl;
 else
  {
    std::cout << "Sent register" << std::endl;
  }

  char buffer2[4096];
  int bytes = recv(sock_descriptor, &(buffer2[0]), 4096, 0);

  for (int i = 0; i < bytes; i++)
	{
		std::cout << "[" << i << "]: " << buffer2[i] << std::endl;
	}
  std::cout << buffer2 << std::endl;

  char buffer3[11] = "load test1";
  buffer3[10] = ext;
 if ( send(sock_descriptor, buffer3, 11, 0) < 0)
    std::cout << strerror(errno) << std::endl;
 else
  {
    std::cout << "Sent load" << std::endl;
  }
 
  char buffer4[4096];
  bytes = recv(sock_descriptor, &(buffer4[0]), 4096, 0);

  for (int i = 0; i < bytes; i++)
	{
		std::cout << "[" << i << "]: " << buffer4[i] << std::endl;
	}
}
