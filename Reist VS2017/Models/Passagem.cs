﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reist_VS2017.Connection;
using MySql.Data.MySqlClient;

namespace Reist_VS2017.Models
{
    public class Passagem
    {
        public string saida { get; set; }
        public Local origem { get; set; }
        public Local destino { get; set; }
        public string chegada { get; set; }
        public string classe { get; set; }
        //public int assentos_economica { get; set; }
        public int assentos { get; set; }
        //public string preco_economica { get; set; }
        public string preco { get; set; }

        public List<Passagem> BuscarIda(string origem, string destino, string data, int classe)
        {
            using (Database DB = new Database())
            {
                string query;
                if (classe == 1 || classe == 2)
                    query = "select * from vw_buscar_passagem_ida where date(saida) = date('" + data + "') and ori_city = '" + origem + "' and des_city = '" + destino + "' and " +
                        "classe = " + classe + "";
                else
                    query = "select * from vw_buscar_passagem_ida where date(saida) = date('" + data + "') and ori_city = '" + origem + "' and des_city = '" + destino + "'";

                var retorno = DB.ReturnCommand(query);
                return Listar(retorno);
            }
        }

        public List<Passagem> Listar(MySqlDataReader retorno)
        {
            var passagens = new List<Passagem>();
            while (retorno.Read())
            {
                var enderecoOrigem = new Endereco()
                {
                    uf = retorno["ori_uf"].ToString(),
                    cidade = retorno["ori_city"].ToString(),
                };

                var enderecoDestino = new Endereco()
                {
                    uf = retorno["des_uf"].ToString(),
                    cidade = retorno["des_city"].ToString(),
                };

                var Origem = new Local()
                {
                    sigla = retorno["origem"].ToString(),
                    endereco = enderecoOrigem
                };

                var Destino = new Local()
                {
                    sigla = retorno["destino"].ToString(),
                    endereco = enderecoDestino
                };

                var passagem = new Passagem()
                {
                    origem = Origem,
                    destino = Destino,
                    saida = retorno["saida"].ToString(),
                    chegada = retorno["chegada"].ToString(),
                    assentos = int.Parse(retorno["assentos"].ToString()),
                    preco = retorno["preco"].ToString(),
                };

                if (int.Parse(retorno["classe"].ToString()) == 2)
                    passagem.classe = "Executiva";
                else
                    passagem.classe = "Econômica";

                passagens.Add(passagem);
            }
            retorno.Close();
            return passagens;
        }
    }
}