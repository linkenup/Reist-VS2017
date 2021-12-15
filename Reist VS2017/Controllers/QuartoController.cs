using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reist_VS2017.Models;

namespace Reist_VS2017.Controllers
{
    public class QuartoController : Controller
    {
        Quarto quarto = new Quarto();

        // GET: Quarto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarQuarto(long id)
        {
            var quartos = quarto.ListarQuartos(id, Session["checkin"].ToString(), Session["checkout"].ToString(), int.Parse(Session["hospedes"].ToString())); 
            return View(quartos);
        }
    }
}