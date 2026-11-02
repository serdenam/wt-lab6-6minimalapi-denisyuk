using wt_lab6_6minimalapi_denisyuk.Models;
using wt_lab6_6minimalapi_denisyuk.Data;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем OpenAPI (для JSON)
builder.Services.AddOpenApi();

// 2. Добавляем NSwag (для графического интерфейса)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config => 
{
    config.Title = "WT-6 Library API";
    config.Version = "v1";
});

var app = builder.Build();

// ==========================================
// Включаем Swagger UI для ВСЕХ режимов (и для Render)
// ==========================================
app.MapOpenApi();
app.UseOpenApi();
app.UseSwaggerUi(); 

// ==========================================
// Эндпоинты
// ==========================================
app.MapGet("/", () => "Minimal API работает!");

app.MapGet("/api/info", () => new
{
    Application = "Web Technologies",
    Technology = "ASP.NET Core Minimal API",
    Version = "1.0",
    Author = "Denisyuk",
    Status = "Published"
});

app.MapGet("/api/books", (string? search) =>
{
    var books = BookStore.Books.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(search))
    {
        books = books.Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                              || b.Author.Contains(search, StringComparison.OrdinalIgnoreCase));
    }
    return Results.Ok(books);
});

app.MapGet("/api/books/{id}", (int id) =>
{
    var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
    if (book == null) return Results.NotFound();
    return Results.Ok(book);
});

app.MapPost("/api/books", (Book newBook) =>
{
    newBook.Id = BookStore.Books.Any() ? BookStore.Books.Max(b => b.Id) + 1 : 1;
    BookStore.Books.Add(newBook);
    return Results.Created($"/api/books/{newBook.Id}", newBook);
});

app.MapPut("/api/books/{id}", (int id, Book updatedBook) =>
{
    var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
    if (book == null) return Results.NotFound();
    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    book.Year = updatedBook.Year;
    book.IsAvailable = updatedBook.IsAvailable;
    return Results.Ok(book);
});

app.MapDelete("/api/books/{id}", (int id) =>
{
    var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
    if (book == null) return Results.NotFound();
    BookStore.Books.Remove(book);
    return Results.NoContent();
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");