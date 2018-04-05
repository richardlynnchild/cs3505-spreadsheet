#include "Server.h"
#include <boost/asio.hpp>
#include <string>
#include <iostream>


Server::server()
{
  std::cout << "Server Started..." << std::endl;
}

void Server::listen_test()
{
  boost::asio::io_service io_service;
  boost::asio::ip::tcp::acceptor acceptor(io_service, 
		boost::asio::ip::tcp::endpoint(boost::asio::ip::tcp::v4(), 
		2112));

  boost::asio::ip::tcp::socket socket(io_service);
  acceptor.accept(socket);
  boost::system::error_code ignored_error;
  boost::asio::write(socket, boost::asio::buffer("hello"), boost::asio::transfer_all(), ignored_error);
}
