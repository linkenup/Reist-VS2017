using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reist_VS2017.Models;

namespace Reist_VS2017.Areas.Funcionario.Controllers
{
    public class PassagemController : Controller
    {
        // GET: Funcionario/Passagem
        public ActionResult CadastroPassagem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Passagem p, string saida, string chegada, string datavolta)
        {
            p.saida = saida;
            p.chegada = chegada;
            p.Inserir();
            return RedirectToAction("Index", "Home");
        }
    }
}