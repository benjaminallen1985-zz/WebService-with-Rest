using System.Xml.Serialization;
using System;

namespace PunClient.Transports
{
    [XmlRoot(Namespace = "http://schemas.datacontract.org/2004/07/Data")]
    public class Pun
    {        
        public int PunID { get; set; }
        public string Title { get; set; }
        public string Joke { get; set; }
    }
}