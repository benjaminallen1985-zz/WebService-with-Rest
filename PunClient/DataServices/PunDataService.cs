using System;
using System.Collections.Generic;
using System.Linq;
using PunClient.Transports;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace PunClient.DataServices
{
    class PunDataService
    {
        public PunDataService()
        {
        }

        public Pun[] GetPuns()
        {
            var client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            var result = client.DownloadString("http://localhost:49944/PunService.svc/Puns");
            var serializer = new DataContractJsonSerializer(typeof(Pun[]));
            Pun[] punArray;
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                punArray = (Pun[])serializer.ReadObject(stream);
            }
            return punArray;
        }

        public Pun GetPunByID(int punID)
        {
            var client = new WebClient();
            var result = client.DownloadString("http://localhost:49944/PunService.svc/Pun/1");
            var serializer = new XmlSerializer(typeof(Pun));
            Pun punObject;
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                punObject = (Pun)serializer.Deserialize(stream);
            }
            return punObject;

        }

        public Pun CreatePun(Pun pun)
        {
            return SendDataToServer("http://localhost:49944/PunService.svc/Puns", "POST", pun);
        }

        private T SendDataToServer<T>(string endpoint, string method, T pun)
        {
            var httpRequest = (HttpWebRequest)HttpWebRequest.Create(endpoint);
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = method;
            var serialize = new DataContractJsonSerializer(typeof(T));
            var requestAStream = httpRequest.GetRequestStream();
            serialize.WriteObject(requestAStream, pun);
            requestAStream.Close();
            var response = httpRequest.GetResponse();
            if (response.ContentLength == 0)
            {
                response.Close();
                return default(T);
            }
            var responseStream = response.GetResponseStream();
            var responseObject = (T)serialize.ReadObject(responseStream);
            responseStream.Close();
            return responseObject;
        }

        public Pun UpdatePun(Pun pun)
        {
            return SendDataToServer("http://localhost:49944/PunService.svc/Pun/" + pun.PunID, "PUT", pun);
        }

        public void DeletePun(int punID)
        {
            SendDataToServer("http://localhost:49944/PunService.svc/Pun/" + punID, "DELETE", 
                            new DeletePun { PunID = punID });
        }
    }
}
