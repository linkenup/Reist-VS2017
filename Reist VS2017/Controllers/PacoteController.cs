using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using Reist_VS2017.Models;

namespace Reist_VS2017.Controllers
{
    public class PacoteController : Controller
    {
        Pacote pacote = new Pacote();
        // GET: Pacote
        public ActionResult Pacote()
        {
            return View();
        }

        /*[HttpGet]
        public ActionResult ListarPacote(string origem, string destino, string dataida, string datavolta)
        {
            Session["origem"] = origem;
            Session["destino"] = destino;
            Session["dataida"] = dataida;
            Session["datavolta"] = datavolta;

            var pacote = passagem./ BuscarIda(origem, destino, dataida, passageiros);

            return
        }*/
    }
}