using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Library library = new Library();

        // Додайте кілька книг до бібліотеки
        library.AddBook(new Book("Три товариші", "Еріх Марія Ремарк", 1936));
        library.AddBook(new Book("1984", "Джордж Оруелл", 1949));
        library.AddBook(new Book("Майстер і Маргарита", "Михайло Булгаков", 1966));

        User user = new User();

        while (true)
        {
            Console.WriteLine("1. Переглянути каталог");
            Console.WriteLine("2. Взяти книгу");
            Console.WriteLine("3. Повернути книгу");
            Console.WriteLine("4. Вийти");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    library.ListAvailableBooks();
                    break;
                case "2":
                    user.BorrowBook(library);
                    break;
                case "3":
                    user.ReturnBook(library);
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}

class Book
{
    public string Title { get; }
    public string Author { get; }
    public int Year { get; }
    public bool IsAvailable { get; private set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
        IsAvailable = true;
    }

    public void Borrow()
    {
        if (IsAvailable)
        {
            IsAvailable = false;
            Console.WriteLine($"Книгу \"{Title}\" взято.");
        }
        else
        {
            Console.WriteLine($"Книгу \"{Title}\" неможливо взяти. Вона вже взята.");
        }
    }

    public void ReturnBook()
    {
        IsAvailable = true;
        Console.WriteLine($"Книгу \"{Title}\" повернуто.");
    }
}

class Library
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(string title)
    {
        Book bookToRemove = books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Книгу \"{title}\" видалено з бібліотеки.");
        }
        else
        {
            Console.WriteLine($"Книга \"{title}\" не знайдена в бібліотеці.");
        }
    }

    public Book SearchBook(string title)
    {
        return books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public void ListAvailableBooks()
    {
        Console.WriteLine("Доступні книги в бібліотеці:");
        foreach (var book in books)
        {
            if (book.IsAvailable)
            {
                Console.WriteLine($"{book.Title} - {book.Author} ({book.Year})");
            }
        }
    }
}

class User
{
    public void BorrowBook(Library library)
    {
        Console.WriteLine("Введіть назву книги, яку ви хочете взяти:");
        string title = Console.ReadLine();
        Book selectedBook = library.SearchBook(title);

        if (selectedBook != null)
        {
            selectedBook.Borrow();
        }
        else
        {
            Console.WriteLine("Книга не знайдена в каталозі бібліотеки.");
        }
    }

    public void ReturnBook(Library library)
    {
        Console.WriteLine("Введіть назву книги, яку ви повертаєте:");
        string title = Console.ReadLine();
        Book selectedBook = library.SearchBook(title);

        if (selectedBook != null)
        {
            selectedBook.ReturnBook();
        }
        else
        {
            Console.WriteLine("Книга не знайдена в каталозі бібліотеки.");
        }
    }
}
