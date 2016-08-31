using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseClient
{
    class AHClient
    {
        static void Main(string[] args)
        {
            SocketSimpleClient client = new SocketSimpleClient("10.140.100.240", 11000);
            client.Run();
        }


    }

    class SocketSimpleClient
    {
        private string servername;
        private int port;

        public SocketSimpleClient(string servername, int port)
        {
            this.servername = servername;
            this.port = port;
        }
        public void Run()
        {
            Console.WriteLine("Simpel client startet on " + servername + " port:" + port);

            //// Instantiér socket - forbinder socket til server
            TcpClient server = new TcpClient(servername, port);

            // opsæt input og output streams
            NetworkStream stream = server.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            string serverData = "";
            string clientData = "";

            while (clientData != "stop")
            {
                // send besked til server
                Console.WriteLine("Writing to server: ");
                clientData = Console.ReadLine();
                writer.WriteLine(clientData); // Skriver tekst til serveren
                writer.Flush(); // Tømmer tcp-bufferen

                // læs svar fra server
                Console.WriteLine("Reading from Server");
                serverData = reader.ReadLine(); // Læser besked fra server
                Console.WriteLine(serverData); // Skriver besked fra skærmen
            }

            Console.WriteLine("Connection to server is closing ...\n");
            writer.Close();
            reader.Close();
            stream.Close();
            server.Close();

            Environment.Exit(0);
        }
    }
}
