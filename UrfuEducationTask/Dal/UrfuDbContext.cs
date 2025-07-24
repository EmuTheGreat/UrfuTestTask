using System.Text.Json;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dal;

public class UrfuDbContext : DbContext
{
    public DbSet<Head> Heads { get; set; }
    public DbSet<Institute> Institutes { get; set; }
    public DbSet<ProgramModel> Programs { get; set; }
    public DbSet<ModuleModel> Modules { get; set; }
    public DbSet<UserModel> Users { get; set; }  // если есть модель UserModel

    public UrfuDbContext(DbContextOptions<UrfuDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProgramModel>()
            .HasOne(p => p.Institute)
            .WithMany(i => i.Programs)
            .HasForeignKey(p => p.InstituteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProgramModel>()
            .HasOne(p => p.Head)
            .WithMany(h => h.Programs)
            .HasForeignKey(p => p.HeadId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ModuleModel>()
            .HasOne(m => m.Program)
            .WithMany(p => p.Modules)
            .HasForeignKey(m => m.ProgramId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}