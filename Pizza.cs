using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    // Classe repr�sentant une Pizza
    public class PizzaEhod
    {
        public int IdEhod { get; set; } // Identifiant unique
        public string? NomEhod { get; set; } // Nom de la pizza
        public string? DescriptionEhod { get; set; } // Description de la pizza
    }

    // Classe DbContext pour g�rer les donn�es en m�moire
    public class PizzaDb : DbContext
    {
        public PizzaDb(DbContextOptions options) : base(options) { }

        public DbSet<PizzaEhod> Pizzas { get; set; } = null!; // Expose la table des pizzas
    }
}
