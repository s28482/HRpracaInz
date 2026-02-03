//DAL - Data Access Layer


using HRpraca.Models;
using Microsoft.EntityFrameworkCore;

namespace HRpraca.DAL;

public class AppDbContext : DbContext
{
    
    public AppDbContext (DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("WMS");
        modelBuilder.UseCollation("Latin1_General_100_CI_AS_SC");
       
        // User -> Role (wymagane, dokladnie jedna rrola na usera
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        //User - unikalny email
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("UX_User_Email");
    }
}