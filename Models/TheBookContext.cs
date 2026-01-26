using Microsoft.EntityFrameworkCore;

namespace Thebook.Models
{
    public class TheBookContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Libro> Libros { get; set; }

        public TheBookContext(DbContextOptions<TheBookContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
