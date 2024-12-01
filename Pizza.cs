namespace PizzaStore.Models
    using Microsoft.EntityFrameworkCore;

{
    public class PizzaEhod
    {
        public int IdEhod { get; set; } // Identifiant unique
        public string? NomEhod { get; set; } // Nom de la pizza
        public string? DescriptionEhod { get; set; } // Description de la pizza
    }

}
