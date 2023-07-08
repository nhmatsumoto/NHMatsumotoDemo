using NHMatsumotoDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NHMatsumotoDemo.Infrastructure.Database.Context
{
    public class MariaDbContext : DbContext
    {
        public MariaDbContext(DbContextOptions<MariaDbContext> options) : base(options) { }

        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MariaDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry =>
                            entry.Entity.GetType().GetProperty("CreatedAt") != null &&
                            entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}

