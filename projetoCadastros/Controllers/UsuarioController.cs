using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace projetoCadastros.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            return View("Usuarios");
        }

        public string ListarUsuario()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("../teste.txt");
            string text = System.IO.File.ReadAllText(path, Encoding.UTF8);

            var strJson = JsonConvert.SerializeObject(text);

            string html = "";
            html += "<div class=\"user-list__item\">" +
                        "<h4>Nome:<span>" + "nome" + "</span></h4>" +
                         "<p>Telefone:<span>" + "telefone" + "</span></p>" +
                         "<p>Logradouro:<span>" + "logradouro" + "</span></p>" +
                         "<p>Complemento:<span>" + "complemento" + "</span></p>" +
                         "<p>Bairro:<span>" + "bairro" + "</span></p>" +
                         "<p>CEP:<span>" + "cep" + "</span></p>" +
                         "<p>Localidade:<span>" + "localidade" + "</span></p>" +
                         "<p>UF:<span>" + "uf" + "</span></p>" +
                    "</div>";
            return html;
        }
    }
}
