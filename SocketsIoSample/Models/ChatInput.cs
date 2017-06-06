using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketsIoSample.Models
{
    public class ChatInput
    {
        public string room { get; set; }
        public string name { get; set; }
        public string message { get; set; }
    }
}
