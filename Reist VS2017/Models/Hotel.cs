﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reist_VS2017.Connection;
using MySql.Data.MySqlClient;

namespace Reist_VS2017.Models
{
    public class Hotel
    {
        public long cnpj { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public Endereco endereco { get; set; }

        public List<Hotel> Buscar(string cidade)
        {
            using (Database DB = new Database())
            {
                var query = "select * from vw_buscar_hotel where cidade = '" + cidade + "';";
                var retorno = DB.ReturnCommand(query);
                return Listar(retorno);
            }
        }


        public List<Hotel> Listar(MySqlDataReader retorno)
        {
            var hoteis = new List<Hotel>();
            while (retorno.Read())
            {
                var endereco = new Endereco()
                {
                    cep = retorno["cep"].ToString(),
                    numero = int.Parse(retorno["numero_endereco"].ToString()),
                    logradouro = retorno["logradouro"].ToString(),
                    bairro = retorno["bairro"].ToString(),
                    cidade = retorno["cidade"].ToString(),
                    uf = retorno["estado"].ToString(),
                };

                var hotel = new Hotel()
                {
                    endereco = endereco,
                    cnpj = long.Parse(retorno["cnpj_hotel"].ToString()),
                    nome = retorno["nome_hotel"].ToString(),
                    descricao = retorno["descricao"].ToString(),
                };
                hoteis.Add(hotel);
            }
            retorno.Close();
            return hoteis;
        }
    }
}