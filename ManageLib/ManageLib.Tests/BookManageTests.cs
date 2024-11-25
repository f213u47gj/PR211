using System;
using System.Linq;
using Management;
using Xunit;

public class BookManageTests
{
    [Fact]
    public void AddBook_ValidBook_AddsSuccessfully()
    {
        var manager = new BookManage();
        var book = new Book { Id = "1", Title = "Book 1", Author = "Author 1", YearPublished = 2020 };

        manager.AddBook(book);

        var result = manager.GetBook("1");
        Assert.Equal(book, result);
    }

    [Fact]
    public void AddBook_DuplicateId_ThrowsException()
    {
        var manager = new BookManage();
        var book = new Book { Id = "1", Title = "Book 1", Author = "Author 1", YearPublished = 2020 };
        manager.AddBook(book);

        var duplicate = new Book { Id = "1", Title = "Duplicate", Author = "Author 2", YearPublished = 2021 };
        Assert.Throws<InvalidOperationException>(() => manager.AddBook(duplicate));
    }

    [Fact]
    public void RemoveBook_ExistingBook_RemovesSuccessfully()
    {
        var manager = new BookManage();
        var book = new Book { Id = "1", Title = "Book 1", Author = "Author 1", YearPublished = 2020 };
        manager.AddBook(book);

        manager.RemoveBook("1");

        Assert.Throws<KeyNotFoundException>(() => manager.GetBook("1"));
    }

    [Fact]
    public void GetBook_NonExistentId_ThrowsException()
    {
        var manager = new BookManage();

        Assert.Throws<KeyNotFoundException>(() => manager.GetBook("100"));
    }

    [Fact]
    public void GetAllBooks_NoBooks_ReturnsEmptyList()
    {
        var manager = new BookManage();

        var books = manager.GetAllBooks();

        Assert.Empty(books);
    }
}
