using Management;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Management
{
    public class BookManage
    {
        private readonly List<Book> _books;

        public BookManage()
        {
            _books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Книга не может быть null.");

            if (_books.Any(b => b.Id == book.Id))
                throw new InvalidOperationException($"Книга с ID {book.Id} уже существует.");

            _books.Add(book);
        }

        public void RemoveBook(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
                throw new ArgumentException("ID книги не может быть пустым или null.", nameof(bookId));

            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new KeyNotFoundException($"Книга с ID {bookId} не найдена.");

            _books.Remove(book);
        }

        public Book GetBook(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
                throw new ArgumentException("ID книги не может быть пустым или null.", nameof(bookId));

            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new KeyNotFoundException($"Книга с ID {bookId} не найдена.");

            return book;
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _books.AsReadOnly();
        }
    }
}
