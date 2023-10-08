using Microsoft.EntityFrameworkCore;

namespace FakeCfdiGenerator.Backend.DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options)
        :base(options)
    {
    }
    public DbSet<Contribuyente> Contribuyentes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contribuyente>()
            .Property(c => c.Id)
            .UseIdentityColumn(1, 1);
        modelBuilder.Entity<Contribuyente>()
            .HasIndex(c => c.Rfc)
            .IsUnique();
    }
}