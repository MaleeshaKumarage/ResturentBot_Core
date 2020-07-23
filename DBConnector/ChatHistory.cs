using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnector
{
    public class ChatHistory
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Utterence { get; set; }
        public string Intent { get; set; }
        //public List<string> Entity { get; set; }
    }
}
