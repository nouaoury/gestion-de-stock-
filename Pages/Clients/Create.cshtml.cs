using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionStock.Models;
using GestionStock.Services;

namespace GestionStock.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseService _databaseService;

        public ClientInfo ClientInfo { get; set; } = new ClientInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public CreateModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void OnGet()
        {
            // Page de création - rien à faire
        }

        public void OnPost()
        {
            try
            {
                // Récupérer les données du formulaire
                ClientInfo.nom = Request.Form["nom"];
                ClientInfo.prenom = Request.Form["prenom"];
                ClientInfo.email = Request.Form["email"];
                ClientInfo.telephone = Request.Form["telephone"];
                ClientInfo.adresse = Request.Form["adresse"];

                // Vérifier que les champs obligatoires ne sont pas vides
                if (string.IsNullOrWhiteSpace(ClientInfo.nom) ||
                    string.IsNullOrWhiteSpace(ClientInfo.prenom) ||
                    string.IsNullOrWhiteSpace(ClientInfo.email))
                {
                    ErrorMessage = "❌ Les champs Nom, Prénom et Email sont obligatoires!";
                    return;
                }

                // Ajouter le client à la BD
                bool success = _databaseService.AddClient(ClientInfo);

                if (success)
                {
                    SuccessMessage = "✅ Client ajouté avec succès!";
                    // Réinitialiser le formulaire
                    ClientInfo = new ClientInfo();
                    // Rediriger vers la liste après 2 secondes
                    Response.Headers.Add("Refresh", "2;url=/Clients/Clients");
                }
                else
                {
                    ErrorMessage = "❌ Erreur lors de l'ajout du client!";
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