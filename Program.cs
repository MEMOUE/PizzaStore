using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Cha�ne de connexion SQLite (vous pouvez la configurer dans appsettings.json ou en hardcodant ici)
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

// Configurer EF Core avec SQLite
builder.Services.AddDbContext<PizzaDb>(options =>
    options.UseSqlite(connectionString)); // Utilisation du fournisseur SQLite

// Ajouter Swagger pour la documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API PizzaStore",
        Description = "Faire les pizzas que vous aimez",
        Version = "v1"
    });
});

var app = builder.Build();

// Activer Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

// Exemple d'endpoint minimal
app.MapGet("/", () => "Bonjour S�n�gal!");

// Ajouter une pizza
app.MapPost("/pizza", async (PizzaDb db, PizzaEhod pizza) =>
{
    await db.Pizzas.AddAsync(pizza); // Ajoute la pizza � la base de donn�es
    await db.SaveChangesAsync(); // Sauvegarde les modifications
    return Results.Created($"/pizza/{pizza.IdEhod}", pizza); // Retourne la pizza cr��e avec son URI
});

// R�cup�rer une pizza par son ID
app.MapGet("/pizza/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));

// R�cup�rer toutes les pizzas
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());

// Mise � jour d'une pizza
app.MapPut("/pizza/{id}", async (PizzaDb db, int id, PizzaEhod pizzaUpdate) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza == null)
    {
        return Results.NotFound(); // Pizza non trouv�e
    }

    pizza.NomEhod = pizzaUpdate.NomEhod;
    pizza.DescriptionEhod = pizzaUpdate.DescriptionEhod;
    pizza.Prix = pizzaUpdate.Prix;
    pizza.Ingredients = pizzaUpdate.Ingredients;

    await db.SaveChangesAsync();
    return Results.NoContent(); // Mise � jour r�ussie
});

// Suppression d'une pizza
app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza == null)
    {
        return Results.NotFound(); // Pizza non trouv�e
    }

    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.NoContent(); // Suppression r�ussie
});

app.Run();
