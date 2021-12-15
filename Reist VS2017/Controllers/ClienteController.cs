using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reist_VS2017.Models;

namespace Reist_VS2017.Controllers
{
    public class ClienteController : Controller
    {
        Cliente cliente = new Cliente();

        // GET: Cliente
        public ActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Cliente c, string nascimento)
        {
            c.nascimento = nascimento;
            c.Inserir();
            return RedirectToAction("Login", "Login");
        }
    }
}