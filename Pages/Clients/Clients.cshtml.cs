using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionStock.Models;
using GestionStock.Services;

namespace GestionStock.Pages.Clients
{
    public class ClientsModel : PageModel
    {
        private readonly DatabaseService _databaseService;
        public List<ClientInfo> listClients { get; set; } = new List<ClientInfo>();
        public string ErrorMessage { get; set; } = "";

        public ClientsModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void OnGet()
        {
            try
            {
                listClients = _databaseService.GetAllClients();
                ErrorMessage = $"Nombre de clients chargés: {listClients.Count}";
                Console.WriteLine(ErrorMessage);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Erreur: " + ex.Message;
                Console.WriteLine("Erreur: " + ex.ToString());
            }
        }
    }
}