using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reist_VS2017.Areas.Funcionario.Controllers
{
    public class LoginController : Controller
    {
        // GET: Funcionario/Login
        public ActionResult Index()
        {
            return View();
        }
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Logout()
        {
            Session["name"] = null;
            return RedirectToAction("Index", "Home", new { area = "Default" });
        }

    }
}