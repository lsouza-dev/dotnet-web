using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Database
{
    internal class ArtistaDAL
    {

        public IEnumerable<Artista> Listar()
        {
            var artistas = new List<Artista>();
            using (SqlConnection connection = new Connection().GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Artistas";
                SqlCommand cmd = new SqlCommand(sql, connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idArtista = Convert.ToInt32(reader["Id"]);
                        string nomeArtista = Convert.ToString(reader["Nome"]);
                        string bioArtista = Convert.ToString(reader["Bio"]);

                        Artista artista = new Artista(nomeArtista, bioArtista) { Id = idArtista };

                        artistas.Add(artista);
                    }
                    return artistas;
                }
            }
        }

        public void Adicionar(Artista artista)
        {
            using(var connection = new Connection().GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO Artistas (Nome,FotoPerfil,Bio) VALUES (@nome,@perfilPadrao,@bio)";
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@nome", artista.Nome);
                cmd.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
                cmd.Parameters.AddWithValue("@bio", artista.Bio);

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"Foram afetadas {rows} após a execução.");
            }
        }


        public Artista ObterPorId(int id)
        {
            using(var connection = new Connection().GetConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM Artistas WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sql,connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        if (reader.Read())
                        {
                            int uid = Convert.ToInt32(reader["Id"]);
                            string nome = Convert.ToString(reader["Nome"])!;
                            string bio = Convert.ToString(reader["Bio"])!;
                            string foto = Convert.ToString(reader["FotoPerfil"]) ?? "";
                            return new Artista(nome,bio) { Id = uid,FotoPerfil = foto};
                        }
                    }
                }
            }
            return null;
        }


        public void Atualizar(Artista artista)
        {
            using (var connection = new Connection().GetConnection())
            {
                connection.Open();
                var sql = "UPDATE Artistas SET Nome = @nome,Bio = @bio,FotoPerfil = @foto WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", artista.Id);

                    if (artista.Nome.Length != 0)
                        cmd.Parameters.AddWithValue("@nome", artista.Nome);

                    if (artista.Bio.Length != 0)
                        cmd.Parameters.AddWithValue("@bio", artista.Bio);

                    if (artista.FotoPerfil.Length != 0)
                        cmd.Parameters.AddWithValue("@foto", artista.FotoPerfil);

                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Foram afetadas {rows} após a execução.");

                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection connection = new Connection().GetConnection())
            {
                connection.Open();
                var sql = "DELETE FROM Artistas WHERE Id = @id";

                using(SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id",id);
                    int rows = cmd.ExecuteNonQuery();

                    Console.WriteLine($"Foram afetadas {rows} após a execução.");
                }
            }
        }
    }
}
