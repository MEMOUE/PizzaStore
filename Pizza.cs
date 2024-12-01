using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    // Classe représentant une Pizza
    public class PizzaEhod
    {
        public int IdEhod { get; set; } // Identifiant unique
        public string? NomEhod { get; set; } // Nom de la pizza
        public string? DescriptionEhod { get; set; } // Description de la pizza
    }

    // Classe DbContext pour gérer les données en mémoire
    public class PizzaDb : DbContext
    {
        public PizzaDb(DbContextOptions options) : base(options) { }

        public DbSet<PizzaEhod> Pizzas { get; set; } = null!; // Expose la table des pizzas
    }
}
