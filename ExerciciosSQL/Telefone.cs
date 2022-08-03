using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CrudBancoSQL
{
    public class Telefone
    {
        public string Ddd { get; set; }
        public string NumTelefone { get; set; }
        public int IdTelefone { get; set; }
        public int IdPessoa { get; set; }

        public Telefone() { }

        public Telefone(string ddd, string numtelefone, int idTelefone, int idPessoa)
        {
            Ddd = ddd;
            NumTelefone = numtelefone;
            IdTelefone = idTelefone;
            IdPessoa = idPessoa;
        }
        public void GravarTelefone() //Salvar o telefone no banco
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                var query = "insert into Telefone (Ddd, NumTelefone, IdPessoa) values (@ddd, @numTelefone, @idPessoa)";
                using (var sql = new SqlConnection(Coneccao))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idPessoa", IdPessoa);
                    command.Parameters.AddWithValue("@ddd", Ddd);
                    command.Parameters.AddWithValue("@numTelefone", NumTelefone);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public Telefone ConfirmarTelefone(int id) //Confirmar o telefone
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";
                using (var cnn = new SqlConnection(Coneccao))
                {
                    string sql = "Select * from Telefone where id = @id";

                    SqlCommand command = new SqlCommand(sql, cnn);
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection.Open();
                    SqlDataReader consulta = command.ExecuteReader();
                    consulta.Read();

                    this.IdTelefone = consulta.GetInt32(0);
                    this.Ddd = consulta.GetString(1);
                    this.NumTelefone = consulta.GetString(2);
                    this.IdPessoa = consulta.GetInt32(3);
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
