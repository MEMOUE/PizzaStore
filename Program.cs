using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurer EF Core avec une base de donn�es en m�moire
builder.Services.AddDbContext<PizzaDb>(options =>
    options.UseInMemoryDatabase("items"));

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

app.MapPost("/pizza", async (PizzaDb db, PizzaEhod pizza) =>
{
    await db.Pizzas.AddAsync(pizza); // Ajoute la pizza � la base de donn�es
    await db.SaveChangesAsync(); // Sauvegarde les modifications
    return Results.Created($"/pizza/{pizza.IdEhod}", pizza); // Retourne la pizza cr��e avec son URI
});

app.MapGet("/pizza/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));

app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());


app.Run();
