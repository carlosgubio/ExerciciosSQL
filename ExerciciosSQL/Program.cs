using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace CrudBancoSQL
{
    enum Opcoes
    {
        Sair,
        CadastrarPessoa,
        ListarPessoas,
        AtualizarPessoa,
        DeletarPessoa,
        CadastrarTelefone,
        AtualizarTelefone,
        DeletarTelefone,
        ListarTodosOsTelefonesPessoas,
        ListarTelefoneUmaPessoa,
        ListarQuantidadeDeTelefonesCadastrados,
        ListarQuantidadeDeTelefonesPorPessoa

    }
    public class Program
    {
        static void Main(string[] args)
        {
            Opcoes opcao;
            Console.WriteLine("======================================");
            Console.WriteLine("Digite a Opção desejada:\n0-Sair\n1-Cadastrar Pessoa\n2-Listar Pessoa\n3-Atualizar Pessoa\n4-Deletar Pessoa\n5-Cadastrar Telefone\n6-Atualizar Telefone\n7-Deletar Telefone\n8-Listar todos os telefones e todas as pessoas\n9-Listar telefone de uma pessoa\n10-Quantidade de telefones cadastrados\n11-quantidade de telefones por pessoa");
            opcao = (Opcoes)Convert.ToInt32(Console.ReadLine());

            while (opcao != Opcoes.Sair)
            {
                if (opcao == Opcoes.CadastrarPessoa) //Cadastrar
                {
                    var pessoa = new Pessoa();
                    Console.WriteLine("Informe o nome:");
                    pessoa.Nome = Console.ReadLine();
                    Console.WriteLine("Informe o CPF:");
                    pessoa.Cpf = Console.ReadLine();
                    Console.WriteLine("Informe o RG:");
                    pessoa.Rg = Console.ReadLine();
                    Console.WriteLine("Informe a Data de Nascimento:");
                    pessoa.DataNascimento = Console.ReadLine();
                    Console.WriteLine("Informe a Naturalidade:");
                    pessoa.Naturalidade = Console.ReadLine();
                    pessoa.GravarPessoa();
                }
                if (opcao == Opcoes.ListarPessoas) //Listar Pessoas
                {
                    ListarPessoas();
                }
                if (opcao == Opcoes.AtualizarPessoa) //Atualizar Pessoa
                {
                    Pessoa pessoa = new Pessoa();
                    Console.WriteLine("Informe o ID:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    pessoa = pessoa.ConfirmarPessoa(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine("O nome cadastrado é: " + pessoa.Nome + "\nDigite o novo nome caso deseje alterar.");
                        string nome = Console.ReadLine();
                        Console.WriteLine("O Cpf cadastrado é: " + pessoa.Cpf + "\nDigite o novo CPF caso deseje alterar.");
                        string cpf = Console.ReadLine();
                        Console.WriteLine("O RG cadastrado é: " + pessoa.Rg + "\nDigite o novo RG caso deseje alterar.");
                        string rg = Console.ReadLine();
                        AtualizarPessoa(id, nome, cpf, rg);
                    }


                }
                if (opcao == Opcoes.DeletarPessoa) //Deletar Pessoa
                {
                    Console.WriteLine("Informe o ID:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Pessoa pessoa = new Pessoa();
                    pessoa = pessoa.ConfirmarPessoa(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine(pessoa.Nome);
                        Console.WriteLine(pessoa.Cpf);
                        Console.WriteLine("Digite 1 se deseja mesmo deletar:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var telefone = AcharTelefoneIDPessoa(id);
                            if (telefone != null)
                            {
                                ExcluirTelefoneIdPEssoa(telefone.IdPessoa);
                                DeletarPessoa(id);
                            }

                        }
                    }
                }
                if (opcao == Opcoes.CadastrarTelefone) //Cadastrar Telefone
                {
                    var telefone = new Telefone();
                    var pessoa = new Pessoa();
                    Console.WriteLine("Digite o Id da Pessoa");
                    int id = Convert.ToInt32(Console.ReadLine());
                    pessoa = pessoa.ConfirmarPessoa(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine(pessoa.Nome);
                        Console.WriteLine("Digite 1 se a pessoa está correta:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            telefone.IdPessoa = id;
                            Console.WriteLine("Digite o DDD");
                            telefone.Ddd = Console.ReadLine();
                            Console.WriteLine("Digite o nº do telefone");
                            telefone.NumTelefone = Console.ReadLine();
                            telefone.GravarTelefone();
                        }
                    }
                }
                if (opcao == Opcoes.AtualizarTelefone) // Atualizar Telefone
                {
                    Pessoa pessoa = new Pessoa();
                    Telefone telefone = new Telefone();
                    Console.WriteLine("Digite o Id da Pessoa");
                    int id = Convert.ToInt32(Console.ReadLine());
                    pessoa = pessoa.ConfirmarPessoa(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine(pessoa.Nome);
                        Console.WriteLine("Digite 1 se a pessoa está correta:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            Console.WriteLine("Digite o Id do telefone");
                            int idTel = Convert.ToInt32(Console.ReadLine());
                            telefone = telefone.ConfirmarTelefone(idTel);
                            Console.WriteLine("O DDD cadastrado é: " + telefone.Ddd + "\nDigite o novo DDD caso deseje alterar.");
                            string ddd = Console.ReadLine();
                            Console.WriteLine("O Telefone cadastrado é: " + telefone.NumTelefone + "\nDigite o novo Telefone caso deseje alterar.");
                            string numTelefone = Console.ReadLine();
                            AtualizarTelefone(id, ddd, numTelefone, idTel);
                        }
                    }
                }
                if (opcao == Opcoes.DeletarTelefone) //Deletar Telefone
                {
                    Pessoa pessoa = new Pessoa();
                    Telefone telefone = new Telefone();
                    Console.WriteLine("Digite o Id da Pessoa");
                    int id = Convert.ToInt32(Console.ReadLine());
                    pessoa = pessoa.ConfirmarPessoa(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine(pessoa.Nome);
                        Console.WriteLine("Digite 1 se a pessoa está correta:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            Console.WriteLine("Digite o Id do telefone");
                            int idTel = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Digite 1 se deseja mesmo deletar:");
                            int confirmaTel = Convert.ToInt32(Console.ReadLine());
                            if (confirmaTel == 1)
                            {
                                DeletarTelefone(idTel);
                            }
                        }
                    }
                }
                if (opcao == Opcoes.ListarTodosOsTelefonesPessoas) //Listar Telefones
                {
                    ListarTelefones();
                }
                if (opcao == Opcoes.ListarTelefoneUmaPessoa)
                {
                    Pessoa pessoa = new Pessoa();
                    Console.WriteLine("Digite o Id da pessoa");
                    int id = Convert.ToInt32(Console.ReadLine());
                    pessoa = pessoa.ConfirmarPessoa(id);
                    ListarTelefonesPessoa(id, pessoa.Nome);
                }
                if (opcao == Opcoes.ListarQuantidadeDeTelefonesCadastrados)
                {
                    int resultado = QuantidadeDeTelefoneCadastrado();
                    if (resultado > 0)
                    {
                        Console.WriteLine(resultado + " telefone(s) cadastrado(s)");
                    }
                    else
                    {
                        Console.WriteLine("Não há telefone(s) cadastrado(s)");
                    }
                }
                if (opcao == Opcoes.ListarQuantidadeDeTelefonesPorPessoa)
                {
                    Console.WriteLine("Digite o Id da pessoa:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var Pessoa = new Pessoa();
                    Pessoa = Pessoa.ConfirmarPessoa(id);
                    if (Pessoa != null)
                    {
                        int resultado = QuantidadeDeTelefonePorPessoa(id);
                        if (resultado > 0)
                        {
                            Console.WriteLine(resultado + " telefone(s) cadastrado(s)");
                        }
                        else
                        {
                            Console.WriteLine("Não há telefone(s) cadastrado(s)");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cadastro não encontrado.");
                    }
                }

                Console.WriteLine("======================================");
                Console.WriteLine("Digite a Opção desejada:0-Sair\n1-Cadastrar Pessoa\n2-Listar Pessoa\n3-Atualizar Pessoa\n4-Deletar Pessoa\n5-Cadastrar Telefone\n6-Atualizar Telefone\n7-Deletar Telefone\n8-Listar todos os telefones e todas as pessoas\n9-Listar telefone de uma pessoa\n10-Quantidade de telefones cadastrados\n11-quantidade de telefones por pessoa");
                opcao = (Opcoes)Convert.ToInt32(Console.ReadLine());
            }

            static void ListarPessoas() //Buscar Pessoa no banco
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";
                    using (var cnn = new SqlConnection(Coneccao))
                    {
                        string sql = "Select * from Pessoa ORDER BY id";

                        SqlCommand command = new SqlCommand(sql, cnn);
                        command.Connection.Open();
                        SqlDataReader consulta = command.ExecuteReader();

                        foreach (DbDataRecord s in consulta)
                        {
                            int id = s.GetInt32(0);
                            string nome = s.GetString(1);
                            string cpf = s.GetString(2);
                            string rg = s.GetString(3);
                            string dataNascimento = s.GetString(4);
                            string naturalidade = s.GetString(5);
                            Console.WriteLine("Codigo: " + id);
                            Console.WriteLine("Nome: " + nome);
                            Console.WriteLine("CPF: " + cpf);
                            Console.WriteLine("Rg: " + rg);
                            Console.WriteLine("Data de nascimento: " + dataNascimento);
                            Console.WriteLine("Naturalidade: " + naturalidade);
                            Console.WriteLine("=======================================");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static void AtualizarPessoa(int id, string nome, string cpf, string rg) //Atualizar Pessoa no banco
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                    var query = "Update Pessoa set Nome = @Nome, Cpf = @Cpf, Rg = @Rg  where id = @Id";
                    using (var sql = new SqlConnection(Coneccao))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@cpf", cpf);
                        command.Parameters.AddWithValue("@rg", rg);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static void AtualizarTelefone(int id, string ddd, string numTelefone, int idTel) //Atualizar Telefone no banco
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                    var query = "Update Telefone set Ddd = @ddd, NumTelefone = @numTelefone where IdPessoa = @Id and id = @idTel";
                    using (var sql = new SqlConnection(Coneccao))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@ddd", ddd);
                        command.Parameters.AddWithValue("@numTelefone", numTelefone);
                        command.Parameters.AddWithValue("@idTel", idTel);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static void DeletarPessoa(int id) //Deletar Pessoa no banco
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                    var query = "Delete From Pessoa where id = @Id";
                    using (var sql = new SqlConnection(Coneccao))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static void DeletarTelefone(int id) //Deletar Telefone no banco
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                    var query = "Delete From Telefone where id = @Id";
                    using (var sql = new SqlConnection(Coneccao))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static void ListarTelefones() //Buscar Telefones no banco
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";
                    using (var cnn = new SqlConnection(Coneccao))
                    {
                        string sql = "select t.Id,t.Ddd,t.NumTelefone,t.IdPessoa,p.Nome from Telefone t , Pessoa p where t.IdPessoa = p.Id";

                        SqlCommand command = new SqlCommand(sql, cnn);
                        command.Connection.Open();
                        SqlDataReader consulta = command.ExecuteReader();

                        foreach (DbDataRecord s in consulta)
                        {
                            int idTelefone = s.GetInt32(0);
                            string ddd = s.GetString(1);
                            string numTelefone = s.GetString(2);
                            int idPessoa = s.GetInt32(3);
                            string nome = s.GetString(4);
                            Console.WriteLine("Nome: " + nome);
                            Console.WriteLine("Codigo: " + idTelefone);
                            Console.WriteLine("DDD: " + ddd);
                            Console.WriteLine("Numero do telefone: " + numTelefone);
                            Console.WriteLine("=======================================");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static void ListarTelefonesPessoa(int id, string nome) //Buscar Telefones de uma determinada pessoa no banco
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";
                    using (var cnn = new SqlConnection(Coneccao))
                    {
                        string sql = "select t.Id,t.Ddd,t.NumTelefone,t.IdPessoa,p.Nome from Telefone t , Pessoa p where t.IdPessoa = @id and p.Nome = @nome";

                        SqlCommand command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Connection.Open();
                        SqlDataReader consulta = command.ExecuteReader();

                        foreach (DbDataRecord s in consulta)
                        {
                            int idTelefone = s.GetInt32(0);
                            string ddd = s.GetString(1);
                            string numTelefone = s.GetString(2);
                            int idPessoa = s.GetInt32(3);
                            string nomes = s.GetString(4);
                            Console.WriteLine("Codigo: " + idTelefone);
                            Console.WriteLine("DDD: " + ddd);
                            Console.WriteLine("Numero do telefone: " + numTelefone);
                            Console.WriteLine("Nome: " + nomes);
                            Console.WriteLine("=======================================");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static Telefone AcharTelefoneIDPessoa(int id)
            {
                Telefone telefone = new Telefone();
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";
                    using (var cnn = new SqlConnection(Coneccao))
                    {
                        string sql = "select * from Telefone where IdPessoa = @id";

                        SqlCommand command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection.Open();
                        SqlDataReader consulta = command.ExecuteReader();
                        consulta.Read();

                        int idTelefone = consulta.GetInt32(0);
                        string ddd = consulta.GetString(1);
                        string numTelefone = consulta.GetString(2);
                        int idPessoa = consulta.GetInt32(3);
                        string nomes = consulta.GetString(4);
                    }
                    return telefone;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                    return null;
                }
            }
            static void ExcluirTelefoneIdPEssoa(int id)
            {
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                    var query = "Delete From Telefone where IdPessoa = @Id";
                    using (var sql = new SqlConnection(Coneccao))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            static int QuantidadeDeTelefoneCadastrado()
            {
                int resultado = 0;
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                    var query = "Select Count (Id) from Telefone";
                    using (var sql = new SqlConnection(Coneccao))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        SqlDataReader consulta = command.ExecuteReader();
                        consulta.Read();
                        resultado = consulta.GetInt32(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
                return resultado;
            }
            static int QuantidadeDeTelefonePorPessoa(int id)
            {
                int resultado = 0;
                try
                {
                    string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AulasCsharp;Data Source=ITELABD02\SQLEXPRESS";

                    var query = "Select Count (Id) from Telefone Where IdPessoa = @id";
                    using (var sql = new SqlConnection(Coneccao))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        SqlDataReader consulta = command.ExecuteReader();
                        consulta.Read();
                        resultado = consulta.GetInt32(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
                return resultado;
            }
        }
    }
}
