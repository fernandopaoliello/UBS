using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UBS.Teste.Services
{
    public class ClienteProcessadoEventArgs
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
        public string Hash { get; set; }
    }
}