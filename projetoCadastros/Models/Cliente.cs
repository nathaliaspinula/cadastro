using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace projetoCadastros.Models
{
    public class Endereco
    {
        [Key]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Status { get; set; }
    }
    public class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; }
        public List<Endereco> Enderecos { get; set; }

        public void AdicionaCliente(FormCollection collection)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Models/Dados/dados.txt");

            string text = System.IO.File.ReadAllText(path, Encoding.UTF8);

            List<Cliente> clientes = (!String.IsNullOrEmpty(text)) ? JsonConvert.DeserializeObject<List<Cliente>>(text) : new List<Cliente>();

            var enderecos = new List<Endereco>();

            for (int i = 1; !String.IsNullOrEmpty(collection["Cep_" + i]); i++)
            {
                enderecos.Add(new Endereco
                {
                    Bairro = collection["Bairro_" + i],
                    Cep = collection["Cep_" + i],
                    Cidade = collection["Cidade_" + i],
                    Complemento = collection["Complemento_" + i],
                    Logradouro = collection["Logradouro_" + i],
                    Numero = collection["Numero_" + i],
                    UF = collection["UF_" + i],
                    Status = "A"
                });
            }

            var cliente = new Cliente
            {
                Id = Guid.NewGuid().ToString(),
                Nome = collection["Nome"],
                Telefone = collection["Telefone"],
                Status = "A",
                Enderecos = enderecos
            };

            while (clientes.Any(c => c.Id == cliente.Id))
            {
                cliente.Id = Guid.NewGuid().ToString();
            }

            clientes.Add(cliente);

            var strObj = JsonConvert.SerializeObject(clientes); ;

            File.WriteAllText(path, strObj);

        }
        internal void AtualizaDados(Cliente clienteExclusao)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Models/Dados/dados.txt");

            string text = System.IO.File.ReadAllText(path, Encoding.UTF8);

            var clientes = ListarClientes();

            clientes.FirstOrDefault(c => c.Id == clienteExclusao.Id).Status = "E";

            var strObj = JsonConvert.SerializeObject(clientes); ;

            File.WriteAllText(path, strObj);
        }

        internal void EditarDados(FormCollection collection)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Models/Dados/dados.txt");

            string text = System.IO.File.ReadAllText(path, Encoding.UTF8);

            var clientes = ListarClientes();

            var clienteEditar = clientes.FirstOrDefault(c => c.Id == collection["Id"]);

            clienteEditar.Nome = collection["Nome"];
            clienteEditar.Telefone = collection["Telefone"];
            var ender = clienteEditar.Enderecos.ToList();
            foreach (var item in ender)
            {
                for (int i = 1; !String.IsNullOrEmpty(collection["Cep_" + i]); i++)
                {
                    item.Bairro = collection["Bairro_" + i];
                    item.Cep = collection["Cep_" + i];
                    item.Cidade = collection["Cidade_" + i];
                    item.Complemento = collection["Complemento_" + i];
                    item.Logradouro = collection["Logradouro_" + i];
                    item.Numero = collection["Numero_" + i];
                    item.UF = collection["UF_" + i];
                }
            }
            var strObj = JsonConvert.SerializeObject(clientes);

            File.WriteAllText(path, strObj);
        }


        public List<Cliente> ListarClientes()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Models/Dados/dados.txt");

            string text = System.IO.File.ReadAllText(path, Encoding.UTF8);

            List<Cliente> clientes = (!String.IsNullOrEmpty(text)) ? JsonConvert.DeserializeObject<List<Cliente>>(text) : new List<Cliente>();

            return clientes;
        }
    }
}