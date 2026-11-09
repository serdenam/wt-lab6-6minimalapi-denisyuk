using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_denisyuk.Models;

namespace wt_lab7_7postgres_efcore_denisyuk.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    { 
    }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой", Year = 1869, IsAvailable = true },
            new Book { Id = 2, Title = "1984", Author = "Джордж Оруэлл", Year = 1949, IsAvailable = true }
        );
    }
}