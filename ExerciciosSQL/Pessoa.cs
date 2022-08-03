using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CrudBancoSQL
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string DataNascimento { get; set; }
        public string Naturalidade { get; set; }

        public Pessoa() { }
        public Pessoa(int id, string nome, string cpf, string rg, string dataNascimento, string naturalidade)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            DataNascimento = dataNascimento;
            Naturalidade = naturalidade;
        }
        public void GravarPessoa() //Salvar o cadastro no banco
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                var query = "insert into Pessoa (Nome, Cpf, Rg, dataNAscimento, Naturalidade) values (@nome,@cpf,@rg,@dataNascimento,@naturalidade)";
                using (var sql = new SqlConnection(Coneccao))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", Nome);
                    command.Parameters.AddWithValue("@cpf", Cpf);
                    command.Parameters.AddWithValue("@rg", Rg);
                    command.Parameters.AddWithValue("@dataNascimento", DataNascimento);
                    command.Parameters.AddWithValue("@naturalidade", Naturalidade);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public Pessoa ConfirmarPessoa(int id) //Confirmar se é a pessoa
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";
                using (var cnn = new SqlConnection(Coneccao))
                {
                    string sql = "Select * from Pessoa where id = @id";

                    SqlCommand command = new SqlCommand(sql, cnn);
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection.Open();
                    SqlDataReader consulta = command.ExecuteReader();
                    consulta.Read();

                    this.Id = consulta.GetInt32(0);
                    this.Nome = consulta.GetString(1);
                    this.Cpf = consulta.GetString(2);
                    this.Rg = consulta.GetString(3);
                    this.DataNascimento = consulta.GetString(4);
                    this.Naturalidade = consulta.GetString(5);
                }
                return this;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                Console.WriteLine("**************************");
                Console.WriteLine("Cadastro não encontrado.");
                Console.WriteLine("**************************");
                return null;
            }
        }
    }
}
