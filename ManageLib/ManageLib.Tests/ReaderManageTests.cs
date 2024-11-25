using System;
using System.Linq;
using Management;
using Xunit;

public class ReaderManageTests
{
    [Fact]
    public void AddReader_ValidReader_AddsSuccessfully()
    {
        var manager = new ReaderManage();
        var reader = new Reader { Id = "1", Name = "Reader 1", Email = "reader1@example.com" };

        manager.AddReader(reader);

        var result = manager.GetReader("1");
        Assert.Equal(reader, result);
    }

    [Fact]
    public void AddReader_DuplicateId_ThrowsException()
    {
        var manager = new ReaderManage();
        var reader = new Reader { Id = "1", Name = "Reader 1", Email = "reader1@example.com" };
        manager.AddReader(reader);

        var duplicate = new Reader { Id = "1", Name = "Duplicate", Email = "duplicate@example.com" };
        Assert.Throws<InvalidOperationException>(() => manager.AddReader(duplicate));
    }

    [Fact]
    public void RemoveReader_ExistingReader_RemovesSuccessfully()
    {
        var manager = new ReaderManage();
        var reader = new Reader { Id = "1", Name = "Reader 1", Email = "reader1@example.com" };
        manager.AddReader(reader);

        manager.RemoveReader("1");

        Assert.Throws<KeyNotFoundException>(() => manager.GetReader("1"));
    }

    [Fact]
    public void RemoveReader_NonExistentId_ThrowsException()
    {
        var manager = new ReaderManage();

        Assert.Throws<KeyNotFoundException>(() => manager.RemoveReader("100"));
    }

    [Fact]
    public void GetReader_NonExistentId_ThrowsException()
    {
        var manager = new ReaderManage();

        Assert.Throws<KeyNotFoundException>(() => manager.GetReader("100"));
    }

    [Fact]
    public void GetAllReaders_NoReaders_ReturnsEmptyList()
    {
        var manager = new ReaderManage();

        var readers = manager.GetAllReaders();

        Assert.Empty(readers);
    }
}
