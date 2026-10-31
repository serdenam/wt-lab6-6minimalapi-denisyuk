using wt_lab6_6minimalapi_denisyuk.Models;
using wt_lab6_6minimalapi_denisyuk.Data;

var builder = WebApplication.CreateBuilder(args);

// --- Блок 6: Регистрация сервиса OpenAPI ---
builder.Services.AddOpenApi();

var app = builder.Build();

// ==========================================
// БЛОК 1. Базовые эндпоинты
// ==========================================

// Уровень 2: Простой GET-запрос
app.MapGet("/", () => "Minimal API работает!");

// Уровень 3: Возврат JSON-объекта
app.MapGet("/api/info", () => 
{
    return new
    {
        Application = "Web Technologies",
        Technology = "ASP.NET Core Minimal API",
        Version = "1.0",
        Author = "Denisyuk"
    };
});

// ==========================================
// БЛОК 3. GET-запросы (Чтение)
// ==========================================

// 6.1 + 6.3: Получение списка с фильтрацией (query-параметр search)
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

// 6.2: Получение объекта по идентификатору
app.MapGet("/api/books/{id}", (int id) => 
{
    var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
    if (book == null) return Results.NotFound();
    return Results.Ok(book);
});

// ==========================================
// БЛОК 4. POST-запрос (Создание)
// ==========================================

app.MapPost("/api/books", (Book newBook) => 
{
    // 7.1. Автоматическая генерация ID (клиент не передает ID)
    newBook.Id = BookStore.Books.Any() ? BookStore.Books.Max(b => b.Id) + 1 : 1;
    
    // Добавляем объект в общую коллекцию
    BookStore.Books.Add(newBook);
    
    // 7.2. Возвращаем статус 201 Created и URL созданного ресурса
    return Results.Created($"/api/books/{newBook.Id}", newBook);
});


// ==========================================
// БЛОК 6. OpenAPI и запуск
// ==========================================

// Подключение эндпоинта OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ==========================================
// БЛОК 5. PUT и DELETE
// ==========================================

// PUT: Обновление книги
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

// DELETE: Удаление книги
app.MapDelete("/api/books/{id}", (int id) => 
{
    var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
    if (book == null) return Results.NotFound();
    
    BookStore.Books.Remove(book);
    return Results.NoContent(); // 204 No Content
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");