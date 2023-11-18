using CyberStrike.Models.DAO;
using CyberStrike.Models.DAO.Generics;
using Microsoft.EntityFrameworkCore;

namespace CyberStrike.Repositories;

public class CyberContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientLocation> ClientLocations { get; set; }

    public CyberContext(DbContextOptions<CyberContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.Entity<Client>()
            .HasIndex(client => new { client.Email, client.UpdatedAt });
        modelBuilder.Entity<Client>()
            .HasAlternateKey(client => new { client.Email });

        modelBuilder
            .Entity<Client>()
            .HasMany<ClientLocation>(cl => cl.ClientLocations);
        
        modelBuilder
            .Entity<ClientLocation>()
            .HasOne<Client>(cl => cl.Client);
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }
    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x is { Entity: Base, State: EntityState.Added or EntityState.Modified });

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((Base)entity.Entity).CreatedAt = now;
            }
            ((Base)entity.Entity).UpdatedAt = now;
        }
    }
}