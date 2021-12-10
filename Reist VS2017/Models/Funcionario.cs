using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Reist_VS2017.Connection;
using MySql.Data.MySqlClient;

namespace Reist_VS2017.Models
{
    public class Funcionario
    {
        public string cpf { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string celular { get; set; }
        public string sexo { get; set; }
        //public DateTime nascimento { get; set; }
        public string nascimento { get; set; }
        public Endereco endereco { get; set; }

        public Funcionario() { }

        public bool Autenticar()
        {
            using (Database database = new Database())
            {
                Hash hash = new Hash(SHA512.Create());
                string command = string.Format("select * from funcionario where email = '{0}';", this.email);
                MySqlDataReader reader = database.ReturnCommand(command);
                reader.Read();

                if (reader.HasRows)
                {
                    if (hash.Verificar(this.senha, reader["senha"].ToString()) == true)
                    {
                        this.nome = reader["nome"].ToString();
                        this.cpf = reader["cpf"].ToString();
                        this.email = reader["email"].ToString();
                        this.senha = reader["senha"].ToString();
                        this.celular = reader["celular"].ToString();
                        this.sexo = reader["sexo"].ToString();

                        /*Endereco enderecoObj = new Endereco();

                        enderecoObj.cep = reader["cep"].ToString();
                        enderecoObj.uf = reader["estado"].ToString();
                        enderecoObj.cidade = reader["cidade"].ToString();
                        enderecoObj.bairro = reader["bairro"].ToString();
                        enderecoObj.logradouro = reader["logradouro"].ToString();
                        enderecoObj.numero = int.Parse(reader["numero_endereco"].ToString());

                        this.endereco = enderecoObj;*/
                        reader.Close(); return true;
                    }
                    else
                        reader.Close(); return false;
                }
                else
                    reader.Close(); return false;
            }
        }
    }
}