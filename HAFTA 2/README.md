# Book API Homework

This project contains a simple ASP.NET Core Web API for managing books. It provides basic CRUD (Create, Read, Update, Delete) operations for books.

## Prerequisites

- .NET Core SDK (version 5.0 or later)
- An API client or testing tool (e.g., Postman)

## Usage

You can use the following API endpoints to interact with the system:

- To retrieve all books, send a `GET /api/book` request.
- To retrieve a specific book, send a `GET /api/book/{id}` request.
- To add a new book, send a `POST /api/book` request with the book details in the request body.
- To update a book, send a `PUT /api/book/{id}` request with the updated book details in the request body.
- To delete a book, send a `DELETE /api/book/{id}` request.
- To list and filter books, send a `GET /api/book/list` request with optional query parameters for filtering (e.g., `name` and `sortBy`).

## Listing and Filtering

You can list and filter books using the `GET /api/book/list` endpoint with the following optional query parameters:

- `title`: Filter books by title. Example: `GET /api/book/list?title=your_title_here`.
- `genre`: Filter books by genre. Example: `GET /api/book/list?genre=your_genre_here`.
- `sort`: Sort the book list by specific criteria. Available options: `title`, `pagecount`, `publishdate`. Example: `GET /api/book/list?sort=title`.

## Authentication

The API supports user authentication using the `/api/user/login` endpoint. You can send a `POST` request with a JSON body containing the `username` and `password` fields to authenticate.

### Sample Request:

```json
{
  "username": "user",
  "password": "pass123"
}
```
