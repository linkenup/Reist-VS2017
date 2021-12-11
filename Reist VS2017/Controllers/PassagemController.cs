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

        [HttpGet]
        public ActionResult ListarPassagem(string origem, string destino, string data)
        {
            var passagens = passagem.BuscarIda(origem, destino, data, 1);
            return View(passagens);
        }
    }
}