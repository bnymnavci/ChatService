using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using ServerData;
using System.IO;

namespace Server
{
    class Server
    {
        static Socket listenerSocket;
        static List<ClientData> clients;

        static void Main(string[] args)   
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  // We identify a listenersocket object.
            clients = new List<ClientData>();

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(Packet.GetIpAddress()), 2022); // We create ipendpoint object with gaven ipaddress and port by host.
            listenerSocket.Bind(ipEndPoint);                                                                                               // We bind ipendpoint to listenersocket object.

            Thread listenThread = new Thread(ListenThread);                         // Create new thread and start this thread.
            listenThread.Start();
        }

        static void ListenThread()
        {
            for(; ; )
            {
                listenerSocket.Listen(0);                                                    // Listen each clients.
                clients.Add(new ClientData(listenerSocket.Accept()));   // The clients are accepted and add to clients list. 
            }
        }

        public static void DataIn(object _socket)
        {
            Socket clientSocket = (Socket)_socket;  // Create a socket object.
            byte[] buffer;
            int readBytes;
            for(; ; )
            {
                buffer = new byte[clientSocket.SendBufferSize];      // We equal defined object buffer size to defined buffer byte array.
                readBytes = clientSocket.Receive(buffer);                                  
                if (readBytes >0) 
                {
                    Packet packet = new Packet(buffer);     // If the data possess received from client, we use packet class for the received data to turn it into a usefull/meaningfull data.
                    DataManager(packet);                             // After that, call datamanager method with returned packet to process.
                }
            }
        }

        public static void DataManager(Packet packet)
        {

        }
    }
}
