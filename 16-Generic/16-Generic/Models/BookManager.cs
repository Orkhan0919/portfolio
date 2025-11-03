namespace _16_Generic.Models;

public class BookManager
{
    public List<Book> Books; 
    public Dictionary<string, List<Book>> BooksByAuthor; 
    public Queue<string> WaitingQueue; 
    public Stack<Book> RecentlyReturned;

    public BookManager(List<Book> books, Dictionary<string, List<Book>> booksByAuthor, 
        Queue<string> waitingQueue, Stack<Book> recentlyReturned)
    {
        Books = books;
        BooksByAuthor = booksByAuthor;
        WaitingQueue = waitingQueue;
        RecentlyReturned = recentlyReturned;
    }

    public void AddBook(Book book)
    {
        Books.Add(book);

        if (!BooksByAuthor.ContainsKey(book.Author))
        {
            BooksByAuthor[book.Author] = new List<Book>();
        }
        
        BooksByAuthor[book.Author].Add(book);
    }

    public List<Book> SearchByTitle(string title)
    {
        List<Book> findBooks = new List<Book>();
        foreach (Book book in Books)
        {
            if (book.Title == title)
            {
                findBooks.Add(book);
            }
        }
        return findBooks; 
    }

    
    public List<Book> GetBooksByAuthor(string author)
    {
        if (BooksByAuthor.ContainsKey(author))
        {
            return BooksByAuthor[author];
        }
        return new List<Book>();
    }

    public void AddWaitingQueue(string memberName)
    {
        WaitingQueue.Enqueue(memberName);
        Console.WriteLine($"{memberName} added to the queue.");
    }
    public string ServeNextInQueue()
    {
        if (WaitingQueue.Count != 0)
        {
            return WaitingQueue.Dequeue();
        }

        return null;
    }

    public void ReturnBook(Book book)
    {
        RecentlyReturned.Push(book);
        Console.WriteLine($"Book returned to the stack: {book.Title} ");
    }
    
    public Book GetLastReturnedBook()
    {
        if (RecentlyReturned.Count > 0)
        {
            return RecentlyReturned.Peek(); 
        }
        return null; 
    }

    
    
    
}