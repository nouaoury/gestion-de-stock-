using GestionStock.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GestionStock.Services
{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetSection("ConnectionStrings")["GestionStockConnection"];
        }

        public List<ClientInfo> GetAllClients()
        {
            List<ClientInfo> clients = new List<ClientInfo>();

            try
            {
                string connectionString = GetConnectionString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT id, nom, prenom, email, telephone, adresse FROM client";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                ClientInfo client = new ClientInfo
                                {
                                    id = rd.GetInt32(0),
                                    nom = rd.GetString(1),
                                    prenom = rd.GetString(2),
                                    email = rd.IsDBNull(3) ? "" : rd.GetString(3),
                                    telephone = rd.IsDBNull(4) ? "" : rd.GetString(4),
                                    adresse = rd.IsDBNull(5) ? "" : rd.GetString(5)
                                };
                                clients.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de connexion: " + ex.Message);
                throw;
            }

            return clients;
        }

        public ClientInfo GetClientById(int id)
        {
            ClientInfo client = null;

            try
            {
                string connectionString = GetConnectionString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT id, nom, prenom, email, telephone, adresse FROM client WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                client = new ClientInfo
                                {
                                    id = rd.GetInt32(0),
                                    nom = rd.GetString(1),
                                    prenom = rd.GetString(2),
                                    email = rd.IsDBNull(3) ? "" : rd.GetString(3),
                                    telephone = rd.IsDBNull(4) ? "" : rd.GetString(4),
                                    adresse = rd.IsDBNull(5) ? "" : rd.GetString(5)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
            }

            return client;
        }

        public bool AddClient(ClientInfo client)
        {
            try
            {
                string connectionString = GetConnectionString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "INSERT INTO client (nom, prenom, email, telephone, adresse) VALUES (@nom, @prenom, @email, @telephone, @adresse)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@nom", client.nom);
                        cmd.Parameters.AddWithValue("@prenom", client.prenom);
                        cmd.Parameters.AddWithValue("@email", client.email ?? "");
                        cmd.Parameters.AddWithValue("@telephone", client.telephone ?? "");
                        cmd.Parameters.AddWithValue("@adresse", client.adresse ?? "");

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                return false;
            }
        }

        public bool UpdateClient(ClientInfo client)
        {
            try
            {
                string connectionString = GetConnectionString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "UPDATE client SET nom = @nom, prenom = @prenom, email = @email, telephone = @telephone, adresse = @adresse WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", client.id);
                        cmd.Parameters.AddWithValue("@nom", client.nom);
                        cmd.Parameters.AddWithValue("@prenom", client.prenom);
                        cmd.Parameters.AddWithValue("@email", client.email ?? "");
                        cmd.Parameters.AddWithValue("@telephone", client.telephone ?? "");
                        cmd.Parameters.AddWithValue("@adresse", client.adresse ?? "");

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                return false;
            }
        }

        public bool DeleteClient(int id)
        {
            try
            {
                string connectionString = GetConnectionString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM client WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                return false;
            }
        }
    }
}