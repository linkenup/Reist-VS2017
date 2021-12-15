using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reist_VS2017.Models;

namespace Reist_VS2017.Controllers
{
    public class HotelController : Controller
    {
        Hotel hotel = new Hotel();

        // GET: Hotel
        public ActionResult Hotel()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListarHotel(string checkin, string checkout, string destino, int quartos, int hospedes)
        {
            Session["destino"] = destino;
            Session["checkin"] = checkin;
            Session["checkout"] = checkout;
            Session["quartos"] = quartos;
            Session["hospedes"] = hospedes;
            var hoteis = hotel.Buscar(destino);

            if (hoteis.Count <= 0)
                return RedirectToAction("NoResult", "Home");
            else
                return View(hoteis);

        }
    }
}