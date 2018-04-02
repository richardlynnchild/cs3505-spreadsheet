//using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking {
    public delegate void NetworkAction(SocketState state);

    /// <summary>
    /// Static class that provides all necessary networking components
    /// to connect to a server, send, and receive data.
    /// </summary>
    public static class Networking {
        private const int DEFAULT_PORT = 11000;

        /// <summary>
        /// Makes a socket with the included parameters.
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="socket"></param>
        /// <param name="ipAddress"></param>

        private static void MakeSocket(string hostName, out Socket socket, out IPAddress ipAddress) {
            ipAddress = IPAddress.None;
            socket = null;
            try {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo;

                // Determine if the server address is a URL or an IP
                try {
                    ipHostInfo = Dns.GetHostEntry(hostName);
                    bool foundIPV4 = false;
                    foreach (IPAddress addr in ipHostInfo.AddressList)
                        if (addr.AddressFamily != AddressFamily.InterNetworkV6) {
                            foundIPV4 = true;
                            ipAddress = addr;
                            break;
                        }
                    // Didn't find any IPV4 addresses
                    if (!foundIPV4) {
                        System.Diagnostics.Debug.WriteLine("Invalid addres: " + hostName);
                        throw new ArgumentException("Invalid address");
                    }
                } catch (Exception) {
                    // see if host name is actually an ipaddress, i.e., 155.99.123.456
                    System.Diagnostics.Debug.WriteLine("using IP");
                    ipAddress = IPAddress.Parse(hostName);
                }

                // Create a TCP/IP socket.
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);

                // Disable Nagle's algorithm - can speed things up for tiny messages, 
                // such as for a game
                socket.NoDelay = true;

            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine("Unable to create socket. Error occured: " + e);
                throw new ArgumentException("Invalid address");
            }
        }

        /// <summary>
        /// Starts the connection process from a socket to a server. Sets a delegate callback
        /// function which is specified by the client to perform actions desired by the client
        /// once a connection has been made.
        /// </summary>
        /// <param name="callbackFunction"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static Socket ConnectToServer(NetworkAction callbackFunction, string hostname) {
            IPAddress addr;
            Socket mySocket;
            MakeSocket(hostname, out mySocket, out addr);

            SocketState state = new SocketState(mySocket);
            state.callMe = callbackFunction;

            mySocket.BeginConnect(addr, DEFAULT_PORT, ConnectedToServer, state);
            return mySocket;
        }

        /// <summary>
        /// This method called once client is connected to server. Called by BeginConnect. Finishes connection
        /// process and then calls the client provided delegate so client can deal with future data receiving/
        /// sending how it wishes.
        /// </summary>
        /// <param name="stateAsArObject"></param>
        private static void ConnectedToServer(IAsyncResult stateAsArObject) {
            SocketState state = (SocketState)stateAsArObject.AsyncState;
            //finish connecting
            state.sock.EndConnect(stateAsArObject);

            //Now call delegate provided by client so it can do what it wants with receiving/sending data
            state.callMe(state);
        }

        /// <summary>
        /// After a connection with server has already been established, client asks server for data.
        /// </summary>
        /// <param name="state"></param>
        public static void GetData(SocketState state) {
            if (state.sock.Connected == false) {
                state.sock.Close();
            }
            state.sock.BeginReceive(state.buffer, 0, state.buffer.Length, SocketFlags.None, ReceiveCallback, state);
        }

        /// <summary>
        /// This gets called after data has been received from server. Called by BeginReceive. Finishes
        /// reception process then provides the data to the SocketState buffer in form of string. Then
        /// client provided delegate is called to process the data how client wishes.
        /// </summary>
        /// <param name="stateAsArObject"></param>
        private static void ReceiveCallback(IAsyncResult stateAsArObject) {
            SocketState myState = (SocketState)stateAsArObject.AsyncState;
            int bytesRead = 0;
            try {
                bytesRead = myState.sock.EndReceive(stateAsArObject);

            } catch (SocketException e) {
                myState.sock.Close();
            }

            //If socket is still open and receiving data
            if (bytesRead > 0) {
                //Decode the data as a string
                string message = Encoding.UTF8.GetString(myState.buffer, 0, bytesRead);

                //Append it to the SocketState StringBuilder
                myState.builder.Append(message);

                //Let the client decide what to do with this newly recieved data
                myState.callMe(myState);
            }
        }



        /// <summary>
        /// Allows client to send data to server.
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        public static void Send(Socket socket, String data) {
            byte[] messageBytes = Encoding.UTF8.GetBytes(data);
            socket.BeginSend(messageBytes, 0, messageBytes.Length, SocketFlags.None, SendCallback, socket);

        }

        /// <summary>
        /// Once data is sent to server, complete sending process. Not much to do here, client just sends
        /// data and lets server deal with it.
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar) {
            Socket sock = (Socket)ar.AsyncState;
            sock.EndSend(ar);
        }
    }
}
