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
        private readonly List<Book> _books;
        private int _nextId = 1; 

        public Library()
        {
            _books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            book.Id = _nextId++;
            _books.Add(book);
            Console.WriteLine($"Книга '{book.Title}' добавлена в библиотеку.");
        }

        public void BorrowBook(int id)
        {

            Book book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Ошибка: Книга с ID {id} не найдена в библиотеке.");
                return;
            }
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
            Book book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Ошибка: Книга с ID {id} не найдена в библиотеке.");
                return;
            }
            if (!book.IsAvailable)
            {
                book.IsAvailable = true;
                Console.WriteLine($"Книга '{book.Title}' успешно возвращена в библиотеку.");
            }
            else
            {
                Console.WriteLine($"Информация: Книга '{book.Title}' уже находится в библиотеке и не требует возврата.");
            }
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
        static void Main()
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

            Console.WriteLine("--- Возвращаем книгу (ID 3) которая не была выдана ---");
            library.ReturnBook(3);
            library.ListBooks();

            Console.WriteLine("--- Пытаемся выдать несуществующую книгу (ID 99) ---");
            library.BorrowBook(99); 

            Console.ReadKey();
        }
    }
}
