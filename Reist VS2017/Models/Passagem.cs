using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reist_VS2017.Connection;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Reist_VS2017.Models
{
    public class Passagem
    {
        public int id { get; set; }
        public string saida { get; set; }
        public Local origem { get; set; }
        public Local destino { get; set; }
        public string origemtext { get; set; }
        public string destinotext { get; set; }
        public string chegada { get; set; } 
        public string classe { get; set; }
        public string empresa { get; set; }
        public int assentos { get; set; }
        public float preco { get; set; }

        public bool TestarData(string data)
        {
            using (Database DB = new Database())
            {
                string query = "call verificar_data('"+data+"', @result); select @result; ";
                var retorno = DB.ReturnCommand(query);
                retorno.Read();
                int result = int.Parse(retorno["@result"].ToString());

                if (result == 1)
                    return false;
                else
                    return true;
            }
        }

        public bool TestarEmpresa(string empresa)
        {
            using (Database DB = new Database())
            {
                string query = "call verificar_empresa('" + empresa + "', @result); select @result; ";
                var retorno = DB.ReturnCommand(query);
                retorno.Read();
                int result = int.Parse(retorno["@result"].ToString());

                if (result == 0)
                    return false;
                else
                    return true;
            }
        }

        public void Inserir()
        {
            using (Database DB = new Database())
            {
                if (this.classe == "Econômica")
                    this.classe = "1";
                if (this.classe == "Executiva")
                    this.classe = "2";


                var query = "call cadastrar_passagem('" + this.saida + " 00:00:00', '" + this.chegada + " 00:00:00', '" + this.origemtext + "', '" + this.destinotext + "', " + this.assentos + ", " + this.classe + ", " + this.preco + ", '" + this.empresa + "');";
                MySqlCommand cmd = new MySqlCommand(query, DB.connection);
                cmd.ExecuteNonQuery();

                //return this;
            }
        }

        public List<Passagem> BuscarIda(string origem, string destino, string data, int passageiros)
        {
            using (Database DB = new Database())
            {
                string query;

                query = "select * from vw_buscar_passagem_ida where date(saida) = date('" + data + "') and ori_city = '" + origem + "' and des_city = '" + destino + "';";

                var retorno = DB.ReturnCommand(query);
                return Listar(retorno, passageiros);
            }
        }

        public List<IdaVolta> BuscarPassagensIdaVolta(string origem, string destino, string dataIda, string dataVolta, int classe, int passageiros)
        {
            using (Database DB = new Database())
            {
                string query;
                if (classe == 1 || classe == 2)
                    query = "select id_ida, id_volta from vw_buscar_passagem_ida_volta where date(saida_ida) = date('" + dataIda + "') and date(saida_volta) = date('" + dataVolta + "') " +
                        "and ori_city = '" + origem + "' and des_city = '" + destino + "' and " +
                        "classe = " + classe + ";";
                else
                    query = "select id_ida, id_volta from vw_buscar_passagem_ida_volta where date(saida_ida) = date('" + dataIda + "') and date(saida_volta) = date('" + dataVolta + "') " +
                        "and ori_city = '" + origem + "' and des_city = '" + destino + "';";
                var retorno = DB.ReturnCommand(query);
                return ListarIdaVolta(retorno, passageiros);
            }
        }

        public void verificar_assentos()
        {
            using (Database database = new Database())
            {
                string command = "call verificar_assentos(" + this.id + ", @assentos_ocupados); select @assentos_ocupados as a;";
                MySqlDataReader retorno = database.ReturnCommand(command);
                retorno.Read();
                int ocupados = int.Parse(retorno["a"].ToString());
                this.assentos = this.assentos - ocupados;
                //return assentos;
            }
        }

        public void CalcularPreco(int pessoas)
        {
            this.preco = this.preco * pessoas;
        }

        public Passagem BuscarPassagem(int id)
        {
            using (Database database = new Database())
            {
                string command = "select * from vw_buscar_passagem_ida where id_passagem = " + id + "";
                MySqlDataReader retorno = database.ReturnCommand(command);
                retorno.Read();

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
                    id = id,
                    origem = Origem,
                    destino = Destino,
                    saida = retorno["saida"].ToString(),
                    chegada = retorno["chegada"].ToString(),
                    assentos = int.Parse(retorno["assentos"].ToString()),
                    preco = float.Parse(retorno["preco"].ToString()),
                };

                if (int.Parse(retorno["classe"].ToString()) == 2)
                    passagem.classe = "Executiva";
                else
                    passagem.classe = "Econômica";
                
                passagem.verificar_assentos();

                return passagem;
            }
        }


        public List<Passagem> Listar(MySqlDataReader retorno, int passageiros)
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
                    id = int.Parse(retorno["id_passagem"].ToString()),
                    origem = Origem,
                    destino = Destino,
                    saida = retorno["saida"].ToString(),
                    empresa = retorno["nome_empresa"].ToString(),
                    chegada = retorno["chegada"].ToString(),
                    assentos = int.Parse(retorno["assentos"].ToString()),
                    preco = float.Parse(retorno["preco"].ToString()),
                };

                if (int.Parse(retorno["classe"].ToString()) == 2)
                    passagem.classe = "Executiva";
                else
                    passagem.classe = "Econômica";

                passagem.verificar_assentos();
                passagem.CalcularPreco(passageiros);

                passagens.Add(passagem);
            }
            retorno.Close();
            return passagens;
        }

        public List<IdaVolta> ListarIdaVolta(MySqlDataReader retorno, int passageiros)
        {
            var idaVoltas = new List<IdaVolta>();
            while (retorno.Read())
            {
                int idIda = int.Parse(retorno["id_ida"].ToString());
                int idVolta = int.Parse(retorno["id_volta"].ToString());

                var passagemIda = BuscarPassagem(idIda);
                var passagemVolta = BuscarPassagem(idVolta);

                var IdaVolta = new IdaVolta()
                {
                    ida = passagemIda,
                    volta = passagemVolta,
                };

                //IdaVolta.PrecoIdaVolta(p);

                idaVoltas.Add(IdaVolta);
            }
            retorno.Close();
            return idaVoltas;
        }
    }

    public class IdaVolta
    {
        public float precoFinal { get; set; }
        public Passagem ida { get; set; }
        public Passagem volta { get; set; }

        public void PrecoIdaVolta(int passageiros)
        {
            ida.CalcularPreco(passageiros);
            volta.CalcularPreco(passageiros);
            
            this.precoFinal = ida.preco + volta.preco;
        }
    }
}