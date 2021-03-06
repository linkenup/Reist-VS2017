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
    public class Cliente
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

        public Cliente() { }

        public void Inserir()
        {
            Hash hash = new Hash(SHA512.Create());

            using (Database DB = new Database())
            {
                if (this.sexo == "Masculino")
                    this.sexo = "M";
                if (this.sexo == "Feminino")
                    this.sexo = "F";
                if (this.sexo == "Prefiro não informar")
                    this.sexo = "X";

                var query = string.Format("call cadastrar_cliente({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', " +
                    "'{9}', '{10}', '{11}','{12}');", this.cpf, this.nome, this.email, hash.Criptografar(this.senha), this.celular, this.sexo, this.nascimento,
                    this.endereco.cep, this.endereco.logradouro, this.endereco.bairro, this.endereco.cidade, this.endereco.uf, this.endereco.numero);

                MySqlCommand cmd = new MySqlCommand(query, DB.connection);
                cmd.ExecuteNonQuery();

                //return this;
            }
        }

        public bool Autenticar()
        {
            using (Database database = new Database())
            {
                Hash hash = new Hash(SHA512.Create());
                string command = string.Format("select * from vw_listar_clientes where email = '{0}';", this.email);
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
                        if (this.sexo == "M")
                            this.sexo = "Masculino";

                        Endereco enderecoObj = new Endereco();

                        enderecoObj.cep = reader["cep"].ToString();
                        enderecoObj.uf = reader["estado"].ToString();
                        enderecoObj.cidade = reader["cidade"].ToString();
                        enderecoObj.bairro = reader["bairro"].ToString();
                        enderecoObj.logradouro = reader["logradouro"].ToString();
                        enderecoObj.numero = int.Parse(reader["numero_endereco"].ToString());

                        this.endereco = enderecoObj;
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

    public class Hash
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
    }
}