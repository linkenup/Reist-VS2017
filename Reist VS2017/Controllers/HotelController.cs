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
        public ActionResult ListarHotel(string checkin, string checkout, string destino)
        {

            var passagens = hotel.Buscar(destino);
            return View(passagens);
        }
    }
}