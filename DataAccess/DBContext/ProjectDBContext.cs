using DataAccess.Entity;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.DBContext
{
    public class ProjectDBContext : IdentityDbContext<UserEntity>
    {
        public ProjectDBContext()
        {
            
        }

        public ProjectDBContext(DbContextOptions options) :base(options) 
        {
            
        }

        public DbSet<ClientEntity> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClientEntity>()
                .ToTable("Clients");

            builder.Entity<UserEntity>()
              .HasQueryFilter(x => !x.IsDeleted);

            builder.Model
              .GetEntityTypes()
              .Where(entityType => typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType) && entityType.BaseType == null)
              .ToList()
              .ForEach(entityType =>
              {
                  var parameter = Expression.Parameter(entityType.ClrType, "entity");

                  var filter = Expression.Lambda(
                      Expression.Equal(
                          Expression.Property(parameter, nameof(ISoftDelete.IsDeleted)),
                          Expression.Constant(false)
                      ),
                      parameter
                  );

                  builder.Entity(entityType.ClrType).HasQueryFilter(filter);
              }
              );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete softEntity)
                {
                    entry.State = EntityState.Modified;
                    softEntity.IsDeleted = true;
                    softEntity.DeletedOn = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
