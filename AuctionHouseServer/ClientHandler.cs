using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    public class ClientHandler
    {
        private Socket clientSocket;

        public ClientHandler(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        public void RunClient()
        {
            NetworkStream netStream = new NetworkStream(clientSocket);
            StreamWriter writer = new StreamWriter(netStream);
            StreamReader reader = new StreamReader(netStream);

            writer.WriteLine("Velkommen til serveren");
            writer.Flush();

            string text;
            text = reader.ReadLine();
            while ((text != null) && (!text.Trim().ToUpper().Equals("BYE")))
            {
                text = "ECHO: <" + text.ToUpper() + ">";
                writer.WriteLine(text);
                writer.Flush();
                text = reader.ReadLine();
            }

            writer.Close();
            reader.Close();
            netStream.Close();
            clientSocket.Close();

        }
    }
}
