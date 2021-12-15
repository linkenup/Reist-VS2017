using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reist_VS2017.Connection;
using Reist_VS2017.Models;
using MySql.Data.MySqlClient;

namespace Reist_VS2017.Controllers
{
    public class CompraController : Controller
    {
        public ActionResult Pagamento(int id)
        {
            Session["idida"] = id;

            /*int passageiros = int.Parse(Session["passageiros"].ToString());            
            Passagem passagem = new Passagem();
            var passagemIda = passagem.BuscarPassagem(id);*/
            
            return View();
        }

        public ActionResult PagamentoHospedagem(int id)
        {

            Session["idquarto"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult FinalizarCompra(Compra compra, string dataValidade)
        {
            Passagem passagem = new Passagem();
            Viagem viagem = new Viagem();
            string id = Session["idida"].ToString();
            compra.cpf = Session["cpf"].ToString();

            passagem = passagem.BuscarPassagem(int.Parse(id));
            viagem.ida = passagem;

            compra.viagem = viagem;
            compra.viagem.quantidade_pessoas = int.Parse(Session["passageiros"].ToString());
            compra.cartao.validade = dataValidade;


            compra.InserirCompraIda();

            return RedirectToAction("Sucesso", "Home");
        }

        [HttpPost]
        public ActionResult FinalizarHospedagem(Compra compra, string dataValidade)
        {
            Quarto quarto = new Quarto();
            Viagem viagem = new Viagem();
            string id = Session["idquarto"].ToString();
            compra.cpf = Session["cpf"].ToString();

            quarto = quarto.BuscarQuarto(int.Parse(id));
            viagem.quarto = quarto;
            viagem.checkin = Session["checkin"].ToString();
            viagem.checkout = Session["checkout"].ToString();

            compra.viagem = viagem;
            compra.viagem.quantidade_pessoas = int.Parse(Session["hospedes"].ToString());
            compra.viagem.quantidade_quartos = int.Parse(Session["quartos"].ToString());

            compra.cartao.validade = dataValidade;


            compra.InserirCompraQuarto();

            return RedirectToAction("Sucesso", "Home");
        }
    }
}