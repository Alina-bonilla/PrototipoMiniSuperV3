using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniSuper.Data;
using MiniSuper;
using System.Reflection;
using MiniSuper.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Obtener la contraseña de la base de datos desde la variable de entorno
string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbPassword))
{
    throw new InvalidOperationException("DB_PASSWORD environment variable is not set.");
}

// Leer la cadena de conexión del archivo de configuración y reemplazar el marcador de posición
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?.Replace("Pw", "Password=" + dbPassword);

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not set.");
}

// Configuración del servicio de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://example.com")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Middleware de CORS
app.UseCors("AllowAnyOrigin");

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Endpoint
app.MapGet("/top3salariobruto", async (ApplicationDbContext dbContext) =>
{
    var top3Empleados = await dbContext.Empleados.FromSqlRaw("EXEC MostrarRankingSalarioBruto").ToListAsync();
    return Results.Ok(top3Empleados);
}).Produces<List<Empleado>>();


app.MapPost("/insertarProducto", async (ApplicationDbContext context, Producto producto) =>
{
    var resultado = await context.Database.ExecuteSqlInterpolatedAsync(
        $"EXEC proceInsertarProducto {producto.IdProducto}, {producto.NombreProducto}, {producto.Costo}, {producto.Precio}");

    if (resultado > 0)
    {
        return Results.Ok(); // Producto insertado con éxito
    }
    else
    {
        return Results.BadRequest(); // Producto no insertado (ya existe uno con el mismo ID)
    }
});

app.Run();