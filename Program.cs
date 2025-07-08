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

    public void RemoveOutdatedBooks(int years)
    {
        int currentYear = DateTime.Now.Year;
        _books.RemoveAll(b => currentYear - b.Year > years);
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

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello world!");
    }
}
