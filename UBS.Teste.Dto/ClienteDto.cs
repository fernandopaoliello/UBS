using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UBS.Teste.Dto
{
    [DataContract]
    public class ClienteDto
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string first_name { get; set; }

        [DataMember]
        public string last_name { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Company { get; set; }

        [DataMember]
        public string Hash { get; set; }

        public string Processado { get; set; }
    }
}
