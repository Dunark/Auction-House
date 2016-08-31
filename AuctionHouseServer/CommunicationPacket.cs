using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class CommunicationPacket
    {
        public List<string> dataTable { get; set; }
        public string logMessage { get; set; }
        public enum Command
        {
            CREATE_BIDDER=1001,
            LOGIN=1002,
            CREATE_AUCTION = 1003,
            MAKE_BID = 1004,
            BID_ERROR = 1005,
            AUCTION_OVERVIEW = 1006,
           
        };

        public CommunicationPacket(List<string> data, string message, Command cmd)
        {
            dataTable = data;
            logMessage = message;
            Command = cmd;
        }


    }

}
