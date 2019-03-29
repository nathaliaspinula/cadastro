using Newtonsoft.Json;
using projetoCadastros.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace projetoCadastros.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View("Cadastro");
        }

        public ActionResult IndexClientes()
        {
            var cliente = new Cliente();
            var clientes = cliente.ListarClientes().Where(c => c.Status == "A").ToList();
            return View("Cliente",clientes);
        }
        public ActionResult Criar(FormCollection collection)
        {
            // Adiciona no ViewBag a mensagem que será exibida quando houver um cadastro realizado
            ViewBag.Cadastro = "Cadastro realizado com sucesso!";

            var cliente = new Cliente();

            cliente.AdicionaCliente(collection);

            return RedirectToAction("../Home/Index");
        }
        public ActionResult ConsultaCep(string cep)
        {
            string url = "http://viacep.com.br/ws/" + cep + "/json/";

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            StreamReader readStream = new StreamReader(myReq.GetResponse().GetResponseStream());

            string data = readStream.ReadToEnd();

            readStream.Close();

            return Json(data);
        }

        public ActionResult ExcluirCliente(string codCliente)
        {
            ViewBag.Cadastro = "Excluído com sucesso";

            var cliente = new Cliente();

            var clientes = cliente.ListarClientes();

            var clienteExclusao = clientes.Where(c => c.Id == codCliente).First();

            cliente.AtualizaDados(clienteExclusao);

            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult EditarCliente(string codCliente)
        {
            var cliente = new Cliente();

            var clientes = cliente.ListarClientes();

            var clienteAlterar = clientes.Where(c => c.Id == codCliente).First();

            return View("Editar",clienteAlterar);
        }

        public ActionResult Editar(FormCollection collection)
        {
            ViewBag.Cadastro = "Alterado com sucesso";

            var cliente = new Cliente();

            cliente.EditarDados(collection);

            return View("~/Views/Home/Index.cshtml");
        }

    }
}
