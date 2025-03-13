using BackEndProyectoSano.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndProyectoSano.DBaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Alimentacion> Alimentaciones { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
