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
        public ActionResult Index()
        {
            return View("Cadastro");
        }
        public JsonResult BuscaCep(string cep) {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + cep + "/json/");
            WebResponse response = request.GetResponse();
            StreamReader readStream = new StreamReader(request.GetResponse().GetResponseStream());
            string data = readStream.ReadToEnd();
            readStream.Close();
            dynamic deserialized = JsonConvert.DeserializeObject(data);
            var serialized = JsonConvert.SerializeObject(deserialized);
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);
            Endereco endereco = new Endereco();
            endereco.cep = values["cep"];
            endereco.logradouro = values["logradouro"];
            endereco.complemento = values["complemento"];
            endereco.bairro = values["bairro"];
            endereco.localidade = values["localidade"];
            endereco.uf = values["uf"];
            return Json(endereco, JsonRequestBehavior.AllowGet);
        }

        public string SalvarDados(FormCollection collection){

            FileInfo cadastro = new FileInfo("c:/cadastro.txt");
            //StreamWriter info = new StreamWriter("c:/ cadastro.txt");
            
            
           
            return "ok";
        }

        public string SalvarCadastros(FormCollection collection) {
            var obj = new
            {
                nome = Request.Form["nome"],
                telefone = Request.Form["telefone"],
                cep = Request.Form["cep"],
                logradouro = Request.Form["logradouro"],
                bairro = Request.Form["bairro"],
                cidade = Request.Form["cidade"],
                localidade = Request.Form["localidade"],
                uf = Request.Form["uf"]
            };
            var strJson = JsonConvert.SerializeObject(obj);

            StreamWriter salvar = new StreamWriter("c:/Users/TRUCKWEB/Documents/teste.txt");

            salvar.Write(strJson);

            salvar.Close();

            return strJson;
        }
    }
}
