# WT-6 Minimal API (Библиотека)

## Таблица API
| Метод | URL | Назначение |
|---|---|---|
| GET | /api/books | получить список книг (с поиском ?search=) |
| GET | /api/books/{id} | получить книгу по ID |
| POST | /api/books | создать книгу |
| PUT | /api/books/{id} | изменить книгу |
| DELETE | /api/books/{id} | удалить книгу |

## Пример JSON для POST/PUT
```json
{
  "title": "Новая книга",
  "author": "Автор",
  "year": 2024,
  "isAvailable": true
}