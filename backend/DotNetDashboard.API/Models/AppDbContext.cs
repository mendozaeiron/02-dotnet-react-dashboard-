using Microsoft.EntityFrameworkCore;

namespace DotNetDashboard.API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Metric> Metrics { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            Email = "admin@dashboard.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            FullName = "Administrator",
            Role = "Admin",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });
        
        modelBuilder.Entity<Metric>().HasData(
            new Metric { Id = 1, Name = "ActiveUsers", Value = 145, Unit = "users", Timestamp = DateTime.UtcNow },
            new Metric { Id = 2, Name = "TotalRevenue", Value = 25430, Unit = "USD", Timestamp = DateTime.UtcNow }
        );
    }
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}

public class Metric
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string? Unit { get; set; }
    public DateTime Timestamp { get; set; }
}
