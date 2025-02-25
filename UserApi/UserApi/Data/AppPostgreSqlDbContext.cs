using Microsoft.EntityFrameworkCore;
using UserApi.Models.Competences;
using UserApi.Models.Files;
using UserApi.Models.Users;

namespace UserApi.Data;

public class AppPostgreSqlDbContext : DbContext
{
    public AppPostgreSqlDbContext(DbContextOptions<AppPostgreSqlDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserField> UserFields { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<AppFile> Files { get; set; }
    public DbSet<Competence> Competences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserField>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserFields)
            .HasForeignKey(x => x.UserId);
        modelBuilder.Entity<UserField>()
            .HasOne(x => x.Field)
            .WithMany()
            .HasForeignKey(x => x.FieldId);
    }

}
