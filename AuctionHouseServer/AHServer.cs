using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class AHServer
    {
        static void Main(string[] args)
        {
            AuctionHouseServer server = new AuctionHouseServer(11500);
            server.tcpListener();
        }
    }

    class AuctionHouseServer
    {
        // Initation of variables
        private IPAddress ip = IPAddress.Parse("127.0.0.1");
        private int port;
        private volatile bool listenerStop = false;


        string textFromClient = "";

        // Set Port
        public AuctionHouseServer(int port)
        {
            this.port = port;
        }

        // Listening to TCP port x,y
        public void tcpListener()
        {
            Console.WriteLine("Starting TCP Listner...");
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            
            while (!listenerStop)
            {
                Socket clientSocket = listener.AcceptSocket();

                Console.WriteLine("IP connected");

                ClientHandler handler = new ClientHandler(clientSocket);
                
                Thread clientTråd = new Thread(handler.RunClient);
                clientTråd.Start();
            }
        }

        // The Socket Client
        public void socketClient()
        {
            inputStreams();

            while (textFromClient != "stop")
            {
                // læs data fra klient
                Console.WriteLine("Reading from Client");
                textFromClient = reader.ReadLine();
                Console.WriteLine("Client:" + textFromClient);

                // Tjek om input er "time?"
                string checkForServertimeInText = "time?";

                if (textFromClient == checkForServertimeInText)
                {
                    string theTime = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
                    Console.WriteLine("Sending time to client : " + theTime);

                    // skriv data til klient
                    writer.WriteLine("server: " + theTime);
                    writer.Flush();
                }
                else
                {
                    // skriv data til klient
                    writer.WriteLine("Server: besked modtaget");
                    writer.Flush();
                }
            }
            exitSocketClient();
        }

        // opsæt input og output streams
        public void inputStreams()
        {
            Console.WriteLine("Setting up network streams...");
            netStream = new NetworkStream(clientSocket);
            writer = new StreamWriter(netStream);
            reader = new StreamReader(netStream);
        }
    }
}
