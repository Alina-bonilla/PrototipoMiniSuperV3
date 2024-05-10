using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniSuper.Data;
using MiniSuper;
using System.Reflection;
using MiniSuper.Models;

var builder = WebApplication.CreateBuilder(args);

// Obtener la contraseña de la base de datos desde la variable de entorno
string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
// Construir la cadena de conexión
string connectionString = $"Server=DESKTOP-2N2DH5F\\SQLSERVER;Database=GestionMiniSuper;User Id=sa;Password={dbPassword};Encrypt=False;Trust Server Certificate=True;";

// Configuración del servicio de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuración del servicio de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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