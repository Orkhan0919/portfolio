using _16_Generic.Models;

Book book1 = new Book(1, "Martin Eden", "Jack London", 1909, 400);
Book book2 = new Book(2, "1984", "George Orwell", 1949, 328);
Book book3 = new Book(3, "Animal Farm", "George Orwell", 1945, 112);
Book book4 = new Book(4, "Ağ Gəmi", "Cingiz Aytmatov", 1970, 200);
Book book5 = new Book(5, "Qırıq Budaq", "Elçin", 1998, 350);

List<Book> allBooks = new List<Book>{book1,book2,book3,book4,book5};

foreach (Book book in allBooks)
{
    book.DisplayInfo();
}

Library<Book> Milli_Kitabkhana = new Library<Book>("Milli Kitabkhana");
Milli_Kitabkhana.AddItem(book1);
Milli_Kitabkhana.AddItem(book2);
Milli_Kitabkhana.AddItem(book3);
Milli_Kitabkhana.AddItem(book4);
Milli_Kitabkhana.AddItem(book5);

Console.WriteLine("Books count in Milli Kitabkhana:");
Milli_Kitabkhana.Count();

Console.WriteLine("Index0 and index2 elements in Milli Kitabkhana:");
Milli_Kitabkhana.FindByIndex(0);
Milli_Kitabkhana.FindByIndex(2);

Milli_Kitabkhana.GetAll();
foreach (Book book in Milli_Kitabkhana.GetAll())
{
    book.DisplayInfo();
}


List<Members> members= new List<Members>();
Members member1 = new Members(1, "Ali Memmedov", "ali@mail.com");
Members member2 = new Members(2, "Leyla Hasanova", "leyla@mail.com");
Members member3 = new Members(3, "Vuqar Aliyev", "vuqar@mail.com");
members.Add(member1);
members.Add(member2);
members.Add(member3);
member1.BorrowBook(book1);
member1.BorrowBook(book2);

Console.WriteLine("Ali Memmedov is borrowed:");
member1.ListBorrowedBooks();

member1.ReturnBook(2);
member1.ListBorrowedBooks();

member1.BorrowBook(book1);
member1.BorrowBook(book2);
member1.BorrowBook(book3);
member1.BorrowBook(book4);


BookManager bookManager = new BookManager(
    new List<Book>(), 
    new Dictionary<string, List<Book>>(), 
    new Queue<string>(), 
    new Stack<Book>()
);
bookManager.AddBook(book1);
bookManager.AddBook(book2);
bookManager.AddBook(book3);
bookManager.AddBook(book4);
bookManager.AddBook(book5);

List<Book> booksByAuthor = bookManager.GetBooksByAuthor("George Orwell");
List<Book> booksByAuthor2 = bookManager.GetBooksByAuthor("Cingiz Aytmatov");
List<Book> booksByAuthor3 = bookManager.GetBooksByAuthor("Dostoyevski");


Console.WriteLine("George Orwell's books :");
foreach (Book book in booksByAuthor)
{
    book.DisplayInfo();
}
Console.WriteLine("Chingiz Aytmatov's books :");
foreach (Book book in booksByAuthor2)
{
    book.DisplayInfo();
}

Console.WriteLine("Dostoyevski's books :");
foreach (Book book in booksByAuthor3)
{
    book.DisplayInfo();
}

Queue<string> waitingQueue = new Queue<string>();
waitingQueue.Enqueue("Nigar");
waitingQueue.Enqueue("Rəşad");
waitingQueue.Enqueue("Səbinə");

Console.WriteLine($"\nWaiting queue {waitingQueue.Count}");

string servedPerson = waitingQueue.Dequeue();
Console.WriteLine($"Serving: {servedPerson}");
Console.WriteLine($"Waiting queue: {waitingQueue.Count}");

servedPerson = waitingQueue.Dequeue();
Console.WriteLine($"Serving: {servedPerson}");
Console.WriteLine($"Waiting queue: {waitingQueue.Count}");

servedPerson = waitingQueue.Dequeue();
Console.WriteLine($"Serving: {servedPerson}");
Console.WriteLine($"Waiting queue: {waitingQueue.Count}");


Stack<Book> returnedBooks = new Stack<Book>();
returnedBooks.Push(book1);
returnedBooks.Push(book2);
returnedBooks.Push(book3);

Console.WriteLine($"\nReturned books: {returnedBooks.Count}");
Console.WriteLine($"Last returned book {returnedBooks.Peek().Title}");

returnedBooks.Pop();
Console.WriteLine($"Returned books: {returnedBooks.Count}");
Console.WriteLine($"Last returned book: {returnedBooks.Peek().Title}");


Book? foundBook = allBooks.FirstOrDefault(b => b.Title == "1984");
if (foundBook != null)
{
    Console.WriteLine($"\nFounded book: {foundBook.Title} ({foundBook.Author}, {foundBook.Year})");
}
else
{
    Console.WriteLine("There's no book!");
}

Book? missingBook = allBooks.FirstOrDefault(b => b.Title == "Harry Potter");
if (missingBook == null)
{
    Console.WriteLine("There's no book for you");
}

int totalBooks = allBooks.Count;
int totalMembers = members.Count;
int waitingCount = waitingQueue.Count;
int stackCount = returnedBooks.Count;

int oldestYear = allBooks[0].Year;
int newestYear = allBooks[0].Year;

foreach (var book in allBooks)
{
    if (book.Year < oldestYear)
        oldestYear = book.Year;
    if (book.Year > newestYear)
        newestYear = book.Year;
}

Console.WriteLine("\n--- STATİSTİC ---");
Console.WriteLine($"Total books: {totalBooks}");
Console.WriteLine($"Total members: {totalMembers}");
Console.WriteLine($"Vaiting queues: {waitingCount}");
Console.WriteLine($"Stack count: {stackCount}");
Console.WriteLine($"Oldest book's year: {oldestYear}");
Console.WriteLine($"Newest book's year: {newestYear}");