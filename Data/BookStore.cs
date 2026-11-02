using wt_lab6_6minimalapi_denisyuk.Models;

namespace wt_lab6_6minimalapi_denisyuk.Data;

public static class BookStore
{
    public static List<Book> Books { get; } = new()
    {
        new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой", Year = 1869, IsAvailable = true },
        new Book { Id = 2, Title = "Преступление и наказание", Author = "Федор Достоевский", Year = 1866, IsAvailable = true },
        new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Year = 1967, IsAvailable = false },
        new Book { Id = 4, Title = "1984", Author = "Джордж Оруэлл", Year = 1949, IsAvailable = true },
        new Book { Id = 5, Title = "Гарри Поттер и философский камень", Author = "Джоан Роулинг", Year = 1997, IsAvailable = false }
    };
}