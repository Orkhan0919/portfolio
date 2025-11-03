namespace _16_Generic.Models;

public class Book
{

    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int PageCount { get; set; }

    public Book(int bookId, string title, string author, int year, int pageCount)
    {
        BookId = bookId;
        Title = title;
        Author = author;
        Year = year;
        PageCount = pageCount;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"{Author} - {Title}({Year}) - Page :{PageCount}");
        
    }
    
    
    
}