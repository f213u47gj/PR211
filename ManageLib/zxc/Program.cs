using Management;
using System;
using System.Text.RegularExpressions;

namespace Management
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookManager = new BookManage();
            var readerManager = new ReaderManage();

            while (true)
            {
                Console.WriteLine("\nМенеджер библиотеки");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Посмотреть книгу");
                Console.WriteLine("3. Посмотреть все книги");
                Console.WriteLine("4. Удалить книгу");
                Console.WriteLine("5. Добавить читателя");
                Console.WriteLine("6. Посмотреть читателя");
                Console.WriteLine("7. Посмотреть всех читателей");
                Console.WriteLine("8. Удалить читателя");
                Console.WriteLine("9. Выйти");

                Console.Write("Выберите действие: ");
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Некорректный ввод. Введите номер действия.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Введите ID книги: ");
                            string bookId = Console.ReadLine();
                            if (!IsNumeric(bookId))
                            {
                                Console.WriteLine("ID книги должен содержать только цифры.");
                                break;
                            }

                            Console.Write("Введите название книги: ");
                            string title = Console.ReadLine();

                            Console.Write("Введите имя автора: ");
                            string author = Console.ReadLine();
                            if (ContainsDigits(author))
                            {
                                Console.WriteLine("Имя автора не может содержать цифры.");
                                break;
                            }

                            Console.Write("Введите год публикации: ");
                            if (!int.TryParse(Console.ReadLine(), out int yearPublished))
                            {
                                Console.WriteLine("Некорректный ввод года публикации.");
                                break;
                            }

                            bookManager.AddBook(new Book { Id = bookId, Title = title, Author = author, YearPublished = yearPublished });
                            Console.WriteLine("Книга успешно добавлена.");
                            break;

                        case 2:
                            Console.Write("Введите ID книги для поиска: ");
                            string searchId = Console.ReadLine();
                            try
                            {
                                Book foundBook = bookManager.GetBook(searchId);
                                Console.WriteLine($"Найдена книга: {foundBook.Title}, автор: {foundBook.Author}, год: {foundBook.YearPublished}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 3:
                            Console.WriteLine("Все книги:");
                            var books = bookManager.GetAllBooks();
                            if (books.Count == 0)
                            {
                                Console.WriteLine("Книг пока нет.");
                            }
                            else
                            {
                                foreach (var book in books)
                                    Console.WriteLine($"ID: {book.Id}, Название: {book.Title}, Автор: {book.Author}, Год: {book.YearPublished}");
                            }
                            break;

                        case 4:
                            Console.Write("Введите ID книги для удаления: ");
                            string removeBookId = Console.ReadLine();
                            try
                            {
                                bookManager.RemoveBook(removeBookId);
                                Console.WriteLine("Книга успешно удалена.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 5:
                            Console.Write("Введите ID читателя: ");
                            string readerId = Console.ReadLine();
                            if (!IsNumeric(readerId))
                            {
                                Console.WriteLine("ID читателя должен содержать только цифры.");
                                break;
                            }

                            Console.Write("Введите имя читателя: ");
                            string name = Console.ReadLine();
                            if (ContainsDigits(name))
                            {
                                Console.WriteLine("Имя читателя не может содержать цифры.");
                                break;
                            }

                            Console.Write("Введите email читателя: ");
                            string email = Console.ReadLine();
                            if (ContainsCyrillic(email))
                            {
                                Console.WriteLine("Email не должен содержать кириллицу.");
                                break;
                            }

                            readerManager.AddReader(new Reader { Id = readerId, Name = name, Email = email });
                            Console.WriteLine("Читатель успешно добавлен.");
                            break;

                        case 6:
                            Console.Write("Введите ID читателя для поиска: ");
                            string readerSearchId = Console.ReadLine();
                            try
                            {
                                Reader foundReader = readerManager.GetReader(readerSearchId);
                                Console.WriteLine($"Найден читатель: {foundReader.Name}, email: {foundReader.Email}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 7:
                            Console.WriteLine("Все читатели:");
                            var readers = readerManager.GetAllReaders();
                            if (readers.Count == 0)
                            {
                                Console.WriteLine("Читателей пока нет.");
                            }
                            else
                            {
                                foreach (var reader in readers)
                                    Console.WriteLine($"ID: {reader.Id}, Имя: {reader.Name}, Email: {reader.Email}");
                            }
                            break;

                        case 8:
                            Console.Write("Введите ID читателя для удаления: ");
                            string removeReaderId = Console.ReadLine();
                            try
                            {
                                readerManager.RemoveReader(removeReaderId);
                                Console.WriteLine("Читатель успешно удалён.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 9:
                            Console.Write("Вы уверены, что хотите выйти? (y/n): ");
                            string confirmation = Console.ReadLine()?.ToLower();
                            if (confirmation == "y")
                            {
                                Console.WriteLine("Выход из программы.");
                                return;
                            }
                            break;

                        default:
                            Console.WriteLine("Некорректный выбор. Попробуйте ещё раз.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        static bool IsNumeric(string input) => Regex.IsMatch(input, @"^\d+$");
        static bool ContainsDigits(string input) => Regex.IsMatch(input, @"\d");
        static bool ContainsCyrillic(string input) => Regex.IsMatch(input, @"[а-яА-Я]");
    }
}
