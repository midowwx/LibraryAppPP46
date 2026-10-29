using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; } = true; 
    }

    public class Library
    {
        private List<Book> _books;
        private int _nextId = 1; 

        public Library()
        {
            _books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            // ОШИБКА 1: Если передать книгу с уже существующим Id, она просто добавится в список.
            book.Id = _nextId++;
            _books.Add(book);
            Console.WriteLine($"Книга '{book.Title}' добавлена в библиотеку.");
        }

        public void BorrowBook(int id)
        {
            // ОШИБКА 2: Отсутствует проверка на существование книги (FindBook может вернуть null).
            // ОШИБКА 3: Если книга не найдена, приложение упадет на следующей строке.
            Book book = _books.Find(b => b.Id == id);

            if (book.IsAvailable)
            {
                book.IsAvailable = false;
                Console.WriteLine($"Книга '{book.Title}' успешно выдана.");
            }
            else
            {
                Console.WriteLine($"Ошибка: Книга '{book.Title}' уже выдана и недоступна.");
            }
        }

        public void ReturnBook(int id)
        {
            // ОШИБКА 4: Аналогично BorrowBook, нет проверки на существование книги.
            // ОШИБКА 5: Метод вернет книгу вне зависимости от того, была ли она выдана.
            Book book = _books.Find(b => b.Id == id);
            book.IsAvailable = true;
            Console.WriteLine($"Книга '{book.Title}' возвращена в библиотеку.");
        }

        public Book FindBook(int id)
        {
            return _books.Find(b => b.Id == id);
        }

        public void ListBooks()
        {
            Console.WriteLine("\nТекущий каталог библиотеки:");
            foreach (var book in _books)
            {
                string status = book.IsAvailable ? "Доступна" : "Выдана";
                Console.WriteLine($"ID: {book.Id} | {book.Title} | {book.Author} | Статус: {status}");
            }
            Console.WriteLine();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            library.AddBook(new Book { Title = "Война и мир", Author = "Л.Н. Толстой" });
            library.AddBook(new Book { Title = "Преступление и наказание", Author = "Ф.М. Достоевский" });
            library.AddBook(new Book { Title = "Мастер и Маргарита", Author = "М.А. Булгаков" });

            library.ListBooks();

            Console.WriteLine("--- Выдаем книгу с ID 1 ---");
            library.BorrowBook(1);
            library.ListBooks();

            Console.WriteLine("--- Пытаемся выдать ту же книгу (ID 1) повторно ---");
            library.BorrowBook(1);

            Console.WriteLine("--- Возвращаем книгу (ID 1) ---");
            library.ReturnBook(1);
            library.ListBooks();

            Console.WriteLine("--- Пытаемся выдать несуществующую книгу (ID 99) ---");
            library.BorrowBook(99); // Здесь приложение упадет

            Console.ReadKey();
        }
    }
}
