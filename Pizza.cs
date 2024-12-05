using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PizzaStore.Models
{
    public class PizzaEhod
    {
        [Key]  // Marquer IdEhod comme clé primaire
        public int IdEhod { get; set; } // Identifiant unique

        public string? NomEhod { get; set; } // Nom de la pizza
        public string? DescriptionEhod { get; set; } // Description de la pizza

        // Ajout de la propriété Prix
        public decimal Prix { get; set; } // Prix de la pizza

        // Ajout de la propriété Ingredients : Liste des ingrédients
        public List<string> Ingredients { get; set; } = new List<string>(); // Liste des ingrédients
    }

    // Classe DbContext pour gérer les données en mémoire
    public class PizzaDb : DbContext
    {
        public PizzaDb(DbContextOptions options) : base(options) { }

        public DbSet<PizzaEhod> Pizzas { get; set; } = null!; // Expose la table des pizzas
    }
}
