using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class UrfuDbContext : DbContext
{
    public DbSet<ProgramModel> Programs   { get; set; }
    public DbSet<ModuleModel>  Modules    { get; set; }
    public DbSet<Institute>    Institutes { get; set; }
    public DbSet<Head>         Heads      { get; set; }
    public DbSet<UserModel>    Users      { get; set; }

    public UrfuDbContext(DbContextOptions<UrfuDbContext> opts) : base(opts) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // явное указание маппинга many-to-many (необязательно для EF Core 5+,
        // он сам сгенерирует join-таблицу)
        modelBuilder.Entity<ProgramModel>()
            .HasMany(p => p.Modules)
            .WithMany(m => m.Programs)
            .UsingEntity(join => join.ToTable("ProgramModules"));
    }
}
