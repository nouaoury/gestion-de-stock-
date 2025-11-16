using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionStock.Models;
using GestionStock.Services;

namespace GestionStock.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly DatabaseService _databaseService;

        public ClientInfo ClientInfo { get; set; } = new ClientInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public EditModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void OnGet()
        {
            try
            {
                // Récupérer l'ID du client depuis l'URL
                string idStr = Request.Query["id"];

                if (string.IsNullOrEmpty(idStr))
                {
                    ErrorMessage = "❌ ID client manquant!";
                    return;
                }

                int id = Convert.ToInt32(idStr);

                // Charger les données du client
                ClientInfo = _databaseService.GetClientById(id);

                if (ClientInfo == null)
                {
                    ErrorMessage = "❌ Client non trouvé!";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "❌ Erreur: " + ex.Message;
                Console.WriteLine(ex.ToString());
            }
        }

        public void OnPost()
        {
            try
            {
                // Récupérer les données du formulaire
                ClientInfo.id = Convert.ToInt32(Request.Form["id"]);
                ClientInfo.nom = Request.Form["nom"];
                ClientInfo.prenom = Request.Form["prenom"];
                ClientInfo.email = Request.Form["email"];
                ClientInfo.telephone = Request.Form["telephone"];
                ClientInfo.adresse = Request.Form["adresse"];

                // Vérifier les champs obligatoires
                if (string.IsNullOrWhiteSpace(ClientInfo.nom) ||
                    string.IsNullOrWhiteSpace(ClientInfo.prenom) ||
                    string.IsNullOrWhiteSpace(ClientInfo.email))
                {
                    ErrorMessage = "❌ Les champs Nom, Prénom et Email sont obligatoires!";
                    return;
                }

                // Mettre à jour le client
                bool success = _databaseService.UpdateClient(ClientInfo);

                if (success)
                {
                    SuccessMessage = "✅ Client modifié avec succès!";
                    Response.Headers.Add("Refresh", "2;url=/Clients/Clients");
                }
                else
                {
                    ErrorMessage = "❌ Erreur lors de la modification!";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "❌ Erreur: " + ex.Message;
                Console.WriteLine(ex.ToString());
            }
        }
    }
}