using System.Text.Json;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dal;

public class UrfuDbContext : DbContext
{
    public DbSet<ProgramModel> Programs { get; set; }
    public DbSet<ModuleModel> Modules { get; set; }
    public DbSet<Institute> Institutes { get; set; }
    public DbSet<Head> Heads { get; set; }
    public DbSet<UserModel> Users { get; set; }

    public UrfuDbContext(DbContextOptions<UrfuDbContext> opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        // Конвертер List<Guid> ↔ JSON
        var jsonConverter = new ValueConverter<List<Guid>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null)!
        );

        mb.Entity<Institute>()
            .Property(i => i.Programs)
            .HasConversion(jsonConverter)
            .HasColumnType("jsonb");
    }
}