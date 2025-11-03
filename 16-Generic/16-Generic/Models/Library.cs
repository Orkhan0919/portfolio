namespace _16_Generic.Models;

public class Library<T>
{
    
    public List<T> Items  { get; set; }
    public string LibraryName { get; set; }

    public Library(string libraryName)
    {
        LibraryName = libraryName;
        Items = new List<T>();
    }

    public void AddItem(T item)
    {
        Items.Add(item);
        Console.WriteLine("Item added");
    }

    public void RemoveItem(T item)
    {
        Items.Remove(item);
        Console.WriteLine("Item removed");          
    }

    public List<T> GetAll()
    {
        return Items;
    }
    public int Count()
    {
        return Items.Count;
    }
    public T FindByIndex(int index)
    {
        if (index >= 0 && index < Items.Count)
        {
            return Items[index];
        }
        else
        {
            throw new IndexOutOfRangeException("Invalid index!");
        }
    }
    
}