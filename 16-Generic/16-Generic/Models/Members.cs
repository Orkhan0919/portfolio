namespace _16_Generic.Models;

public class Members 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Book> BorrowedBooks { get; set; }
    
    public Members(int id, string name,string email)
        
    {
        Id = id;
        Name = name;
        Email = email;
        BorrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book)
    {
        if(BorrowedBooks.Count>3)
        {
            Console.WriteLine("Maksimum 3 kitab goture bilersiniz !");
        }
        else if(BorrowedBooks.Count<=3)
        {
            BorrowedBooks.Add(book);
            Console.WriteLine($"Kitab elave edildi : {book.Title}");
        }
    }

    public void ReturnBook(int bookId)
    {
        Book bookToReturn = null;

        foreach (Book b in BorrowedBooks)
        {
            if (b.BookId == bookId)
            {
                bookToReturn = b;
                break;
            }
        }

        if (bookToReturn != null)
        {
            BorrowedBooks.Remove(bookToReturn);
            Console.WriteLine($"Book {bookToReturn.Title} returned");
        }
        else
        {
            Console.WriteLine($"No book found with ID {bookId}");
        }
    }


    public void DisplayBorrowedBooks()
    {
        if (BorrowedBooks.Count == 0)
        {
            Console.WriteLine("Borc kitabiniz yoxdur .");
        } 
        else if (BorrowedBooks.Count >= 3)
        {
            Console.Write("Borc kitablariniz :");
            
            foreach (Book book in BorrowedBooks)
            {
                Console.WriteLine(book.Title);
            }
        }   
    }

    public void ListBorrowedBooks( )
    {
        foreach (Book book in BorrowedBooks)
        {
            book.DisplayInfo();
        }
    }
    
}

