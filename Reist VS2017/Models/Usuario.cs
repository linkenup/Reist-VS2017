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
    public class Usuario
    {
        public int id { get; set; }
        public string username { get; set; }
        public string senha { get; set; }
        public int nivel { get; set; }

        public bool Autenticar()
        {
            using (Database database = new Database())
            {
                Hash hash = new Hash(SHA512.Create());
                //this.senha = hash.Criptografar(this.senha);

                string command = string.Format("select * from cliente where email = '{0}';", this.username);
                MySqlDataReader reader = database.ReturnCommand(command);
                reader.Read();

                if (reader.HasRows)
                {
                    this.nivel = 0;
                    return hash.Verificar(this.senha, reader["senha"].ToString());
                }
                else
                {
                    command = string.Format("select * from funcionario where email = '{0}';", this.username);
                    reader = database.ReturnCommand(command); reader.Read();
                    if (reader.HasRows)
                    {
                        this.nivel = int.Parse(reader["acesso"].ToString());
                        return hash.Verificar(this.senha, reader["senha"].ToString());
                    }
                    else
                        reader.Close(); return false;
                }
            }
        }
    }

    /*public class Hash
    {
        private HashAlgorithm algorithm;
        public Hash(HashAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        //
        public string Criptografar(string senha)
        {
            var codificado = Encoding.UTF8.GetBytes(senha);
            var criptografado = this.algorithm.ComputeHash(codificado);

            var builder = new StringBuilder();
            foreach (var caracter in criptografado)
            {
                builder.Append(caracter.ToString("X2"));
            }

            return builder.ToString();
        }

        //
        public bool Verificar(string senhaDigitada, string senhaCodificada)
        {
            return this.Criptografar(senhaDigitada) == senhaCodificada;
        }
    }*/
}