using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniSuper.Data;
using MiniSuper;
using System.Reflection;
using MiniSuper.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración del servicio de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
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
    Console.WriteLine(top3Empleados.ToString());
    return Results.Ok(top3Empleados);
}).Produces<List<Empleado>>();


app.MapPost("/insertarProducto", async (ApplicationDbContext context, Producto producto) =>
{
    Console.WriteLine("Entro");
    var resultado = await context.Database.ExecuteSqlInterpolatedAsync(
        $"EXEC proceInsertarProducto {producto.IdProducto}, {producto.NombreProducto}, {producto.Costo}, {producto.Precio}");

    if (resultado > 0)
    {
        // Regla C# 5445
        var tempPath = Path.GetTempFileName();
        using (var writer = new StreamWriter(tempPath))
        {
            await writer.WriteLineAsync($"Se insertó el producto: {producto.IdProducto}");
        }

        return Results.Ok(); // Producto insertado con éxito
    }
    else
    {
        return Results.BadRequest(); // Producto no insertado (ya existe uno con el mismo ID)
    }

    // Regla C# 5146, solo disponible en versionDeveloper 
    app.MapGet("/redirect", async (string url) =>
    {
        // Regla C# 1481
        int i = 2;
        return await Task.FromResult(Results.Redirect(url));
    });
});

app.Run();