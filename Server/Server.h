#ifndef SERVER_H
#define SERVER_H

#include <boost/asio.hpp>
#include <string>

class server
{
  private:
    void send_full_state(boost::asio::ip::tcp::socket client_sock);

  public:
    void listen_test();
    server();
};

#endif SERVER_H
