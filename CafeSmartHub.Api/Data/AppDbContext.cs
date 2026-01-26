using CafeSmartHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeSmartHub.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CategoriaProducto> CategoriasProducto => Set<CategoriaProducto>();
    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>()
            .HasIndex(c => c.Nombre)
            .IsUnique();

        modelBuilder.Entity<Producto>()
            .HasIndex(p => p.Nombre);

        modelBuilder.Entity<Producto>()
            .HasOne(p => p.CategoriaProducto)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaProductoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
