#include <sys/socket.h>
#include <stdio.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>
#include <iostream>

int main(int argc, char const *argv[]){
	
	struct sockaddr_in address;
	int port = 2112;
	int opt = 1;
	int addrlen = sizeof(address);
	char buffer[1024] = {0};
	char* hello = "Hello from server";
	int server_fd = socket(AF_INET, SOCK_STREAM, 0);
	std::cout << "Created socket with fd: " << server_fd << std::endl;
	setsockopt(server_fd, SOL_SOCKET, SO_REUSEADDR | SO_REUSEPORT, &opt, sizeof(opt));
	address.sin_family = AF_INET;
	address.sin_addr.s_addr = INADDR_ANY;
	address.sin_port = htons(port);
	bind(server_fd, (struct sockaddr *) &address, sizeof(address));
	std::cout << "Waiting for clients to connect..." << std::endl;
	listen(server_fd, 3);
	int new_socket = accept(server_fd, (struct sockaddr *) &address, (socklen_t *) &addrlen);
	std::cout << "Client connected!" << std::endl;
	//int valread = read(new_socket, buffer, 1024);
	send(new_socket, hello, strlen(hello), 0);
	return 0;

}