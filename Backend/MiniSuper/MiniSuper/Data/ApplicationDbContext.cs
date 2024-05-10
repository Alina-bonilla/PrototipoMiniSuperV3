using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSuper.Models;

namespace MiniSuper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }

    }
}

