public abstract class Book
{
    public string ISBN { get; }
    public string Title { get; }
    public int Year { get; }
    public double Price { get; protected set; }
    protected Book(string isbn, string title, int year, double price)
    {
        ISBN = isbn;
        Title = title;
        Year = year;
        Price = price;
    }
}

public class PaperBook: Book
{
    public int Stock { get; private set; }

    public PaperBook(string isbn, string title, int year, double price, int stock)
        : base(isbn, title, year, price)
    {
        Stock = stock;
    }

    public void DecreaseStock(int quantity)
    {
        if (Stock < quantity) throw new Exception("Not enough stock.");
        Stock -= quantity;
    }
}

public class EBook: Book
{
    public string FileType { get;}

    public EBook(string isbn, string title, int year, double price, string fileType)
        : base(isbn, title, year, price)
    {
        FileType = fileType;
    }
}

public class ShowcaseBook : Book
{
    public ShowcaseBook(string isbn, string title, int year) : base(isbn, title, year, 0) { }
}

public static class ShippingService
{
    public static void Send(PaperBook book,int quantity, string address)
    {
        Console.WriteLine($"Shipping {quantity} of '{book.Title}' to this address >> {address}.");
    }
}

public static class MailService
{
    public static void Send(EBook book, string email)
    {
        Console.WriteLine($"Sent '{book.Title}' to {email}.");
    }
}

public class Bookstore
{
    private List<Book> _books = new List<Book>();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public Book? FindBook(string isbn)
    {
        return _books.FirstOrDefault(b => b.ISBN == isbn);
    }

    public List<Book> RemoveOutdatedBooks(int years)
    {
        int currentYear = DateTime.Now.Year;
        var outdatedBooks = _books.Where(b => currentYear - b.Year > years).ToList();
        _books.RemoveAll(b => currentYear - b.Year > years);
        return outdatedBooks;
    }

    public double BuyBook(string isbn, int quantity, string email, string address)
    {
        var book = FindBook(isbn);

        if (book == null) throw new Exception("Book is not found.");

        if (book is PaperBook paperBook)
        {
            paperBook.DecreaseStock(quantity);
            ShippingService.Send(paperBook, quantity, address);
            return paperBook.Price * quantity;
        }

        if (book is EBook ebook)
        {
            MailService.Send(ebook, email);
            return ebook.Price * quantity;
        }
        throw new Exception("Book type not supported for purchase.");
    }
}

public class Tests
{
    public static void Run()
    {
        var bookstore = new Bookstore();
        bookstore.AddBook(new PaperBook("123", "A Game of Thrones", 1995, 25, 10));
        bookstore.AddBook(new PaperBook("456", "Head First C#", 2010, 35.5, 5));
        bookstore.AddBook(new EBook("789", "Grokking Algorithms", 2016, 29.99, "PDF"));
        bookstore.AddBook(new EBook("101", "A Song of Ice and Fire", 2011, 49.99, "EPUB"));
        bookstore.AddBook(new ShowcaseBook("202", "Showcase Book", 1700));

        Console.WriteLine("\nBuying books...");
        try
        {
            double paid = bookstore.BuyBook("123", 2, "hossam.sabeer55@gmail.com", "6th of October");
            Console.WriteLine($"Paid: {paid:C}");

            paid = bookstore.BuyBook("789", 1, "hossam.sabeer55@gmail.com", "6th of October");
            Console.WriteLine($"Paid: {paid:C}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Console.WriteLine("\nTrying to buy a showcase book...");
        try
        {
            bookstore.BuyBook("202", 1, "hossam.sabeer55@gmail.com", "6th of October");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Console.WriteLine("\nRemoving outdated books (older than 50 years)...");
        var removedBooks = bookstore.RemoveOutdatedBooks(50);
        if (removedBooks.Count > 0)
        {
            Console.WriteLine($"Removed {removedBooks.Count} outdated book(s):");
            foreach (var book in removedBooks)
            {
                Console.WriteLine($"- {book.Title} (ISBN: {book.ISBN}, Year: {book.Year})");
            }
        }
        else Console.WriteLine("No outdated books to remove.");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Tests.Run();
    }
}