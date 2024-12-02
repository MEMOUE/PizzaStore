using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurer EF Core avec une base de données en mémoire
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
app.MapGet("/", () => "Bonjour Sénégal!");

app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());


app.Run();
