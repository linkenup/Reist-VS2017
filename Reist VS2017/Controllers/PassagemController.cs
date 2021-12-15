using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reist_VS2017.Models;
using Newtonsoft.Json;

namespace Reist_VS2017.Controllers
{
    public class PassagemController : Controller
    {
        Passagem passagem = new Passagem();

        // GET: Passagem
        public ActionResult Passagem()
        {
            return View();
        }

        public ActionResult ListarIdaVolta(Passagem passagem)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListarPassagem(string origem, string destino, string dataida, string datavolta, string checkvolta, int passageiros)
        {
            Session["origem"] = origem;
            Session["destino"] = destino;
            Session["dataida"] = dataida;
            Session["datavolta"] = datavolta;
            Session["passageiros"] = passageiros;

            if (checkvolta == "volta")
            {
                var passagens = passagem.BuscarIda(origem, destino, dataida, passageiros);
                passagem.CalcularPreco(passageiros);
                if (passagens.Count <= 0)
                    return RedirectToAction("NoResult", "Home");
                else
                    return RedirectToAction("ListarIdaVolta", "Passagem", passagem);
            }
            else
            {
                var passagens = passagem.BuscarIda(origem, destino, dataida, passageiros);
                if (passagens.Count <= 0)
                    return RedirectToAction("NoResult", "Home");
                else
                    return View(passagens);
            }
        }

        /*[HttpGet]
        public ActionResult ListarPassagemIdaVolta(string origem, string destino, string dataIda, string dataVolta)
        {
            var passagens = passagem.BuscarPassagensIdaVolta(origem, destino, dataIda, dataVolta);

            if (passagens.Count > 0)
                return View(passagens);
            else
                return RedirectToAction("SemResultados", "Passagem");
        }*/
    }
}