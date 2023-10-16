using CyberStrike.Models.DAO;
using CyberStrike.Models.DAO.Bases;
using Microsoft.EntityFrameworkCore;

namespace CyberStrike.Repository;

public class CyberContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasOne<Profile>();
        modelBuilder
            .Entity<Profile>()
            .HasOne<User>();
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