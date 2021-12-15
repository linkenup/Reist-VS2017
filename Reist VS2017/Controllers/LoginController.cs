using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reist_VS2017.Models;

namespace Reist_VS2017.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Cliente cliente)
        {
            if (cliente.Autenticar() == true)
            {
                Session["cpf"] = cliente.cpf;
                Session["name"] = cliente.nome;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Funcionario funcionario = new Funcionario();
                funcionario.email = cliente.email; funcionario.senha = cliente.senha;
                if (funcionario.Autenticar() == true)
                {
                    Session["name"] = funcionario.nome;
                    return RedirectToAction("Index", "Home", new { area = "Funcionario" });
                }
            }

                return RedirectToAction("Contact", "Home");
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Logout()
        {
            Session["name"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}