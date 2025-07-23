using System.Text.Json;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1) Конвертер List<Guid> <-> JSON
        var jsonConverter = new ValueConverter<List<Guid>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new List<Guid>());

        // 2) Компаратор списков
        var listComparer = new ValueComparer<List<Guid>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        );

        // 3) Настраиваем поле Programs в Institute
        var instProp = modelBuilder.Entity<Institute>()
            .Property(i => i.Programs);

        instProp.HasConversion(jsonConverter);
        instProp.HasColumnType("jsonb");

        // поскольку HasValueComparer недоступен, делаем так:
        instProp.Metadata.SetValueComparer(listComparer);

        // 4) То же для ModuleIds в ProgramModel
        var progProp = modelBuilder.Entity<ProgramModel>()
            .Property(p => p.ModuleIds);

        progProp.HasConversion(jsonConverter);
        progProp.HasColumnType("jsonb");
        progProp.Metadata.SetValueComparer(listComparer);
    }
}