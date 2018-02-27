using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Data;

namespace WebService.REST
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PunService
    {
        private PunDataService _service;

        public PunService()
        {
            _service = new PunDataService();
        }

        [WebGet(UriTemplate="/Puns")]
        public Pun[] GetPuns()
        {
            return _service.GetPuns();
        }

        [WebGet(UriTemplate = "/Pun/{punID}")]
        public Pun GetPunByID(string punID)
        {
            int parseID;
            Int32.TryParse(punID, out parseID);
            return _service.GetPunById(parseID);
        }

        [WebInvoke(UriTemplate = "/Puns")]
        public Pun CreatePun(Pun pun)
        {
            return _service.AddPun(pun);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/Pun/{punID}")]
        public Pun UpdatePun(string punID, Pun pun)
        {
            return _service.UpdatePun(pun);
        }

        [WebInvoke(Method = "DELETE", UriTemplate = "/Pun/{punID}")]
        public void DeletePun(string punID)
        {
            int parseID;
            Int32.TryParse(punID, out parseID);
            _service.GetPunById(parseID);
        }
    }
}
