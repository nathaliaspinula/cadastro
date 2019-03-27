using Newtonsoft.Json;
using projetoCadastros.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace projetoCadastros.Controllers
{
    public class CadastroController : Controller
    {
        public string Index() {

            return ("ok");

        }
        public JsonResult BuscaCep(string cep) {
            
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + cep + "/json/");
            
            WebResponse response = request.GetResponse();

            StreamReader readStream = new StreamReader(request.GetResponse().GetResponseStream());

            string data = readStream.ReadToEnd();

            readStream.Close();

            Console.WriteLine(data);
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
