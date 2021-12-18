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
        Passagem passagem = new Passagem();

        // GET: Funcionario/Passagem
        public ActionResult CadastroPassagem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroPassagem(Passagem p, string saida, string chegada)
        {
            p.saida = saida;
            p.chegada = chegada;
            Session["saidacad"] = saida;

            if (!ModelState.IsValid && p.TestarData(saida) == false || p.TestarData(saida) == false)
            {
                Session["testedate"] = "Data inválida";
                return View(p);
            }

            if (p.TestarData(saida) == false && p.TestarEmpresa(p.empresa) == false)
            {
                Session["testeempresa"] = "Empresa não resgistrada";
                Session["testedate"] = "Data inválida";
                return View(p);
            }

            if (!ModelState.IsValid && p.TestarEmpresa(p.empresa) == false || p.TestarEmpresa(p.empresa) == false)
            {
                Session["testeempresa"] = "Empresa não resgistrada";
                return View(p);
            }

            if (!ModelState.IsValid)
            {
                Session["testedate"] = null;
                Session["testeempresa"] = null;
                return View(p);
            }

            Session["saidacad"] = null;
            p.Inserir();
            return RedirectToAction("Index", "Home");
        }
    }
}