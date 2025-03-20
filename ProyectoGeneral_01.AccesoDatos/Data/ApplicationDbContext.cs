using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categoria { get; set; }

    public DbSet<Articulo> Articulo { get; set; }

    public DbSet<Slider> Slider { get; set; }
}
