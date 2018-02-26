using System;
using System.Runtime.Serialization;

namespace Data
{
    [Serializable]
    [DataContract]
    public class Pun
    {
        [DataMember]
        public int PunID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Joke { get; set; }
    }
}