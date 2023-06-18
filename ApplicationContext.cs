using Destinationosh.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<FileModel> Files { get; set; } = null!;
    public DbSet<PostVisit> PostVisits { get; set; } = null!;
    public DbSet<RouteModel> Routes { get; set; } = null!;
    public DbSet<ComponentConfig> ComponentConfigs { get; set; } = null!;
    public DbSet<CustomTable> CustomTables { get; set; } = null!;
    public DbSet<CustomTableRow> CustomTableRows { get; set; } = null!;
    public DbSet<CustomField> CustomFields { get; set; } = null!;
    public DbSet<CustomValue> CustomValues { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<User>()
            .Property(user => user.Role)
            .HasConversion(new EnumToStringConverter<UserRole>());

        modelBuilder
            .Entity<RouteModel>(entity =>
            {
            });

    }
}