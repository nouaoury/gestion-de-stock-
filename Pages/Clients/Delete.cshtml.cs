using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionStock.Services;

namespace GestionStock.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly DatabaseService _databaseService;

        public DeleteModel(DatabaseService databaseService)
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
                    throw new Exception("ID client manquant!");
                }

                int id = Convert.ToInt32(idStr);

                // Supprimer le client
                bool success = _databaseService.DeleteClient(id);

                if (success)
                {
                    Console.WriteLine($"Client {id} supprimé avec succès");
                }
                else
                {
                    Console.WriteLine($"Erreur lors de la suppression du client {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de suppression: " + ex.Message);
            }

            // Rediriger vers la liste
            Response.Redirect("/Clients/Clients");
        }
    }
}