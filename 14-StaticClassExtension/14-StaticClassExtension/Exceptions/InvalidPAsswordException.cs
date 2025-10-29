namespace _14_StaticClassExtension.Models;

public class InvalidPAsswordException:Exception
{
    public InvalidPAsswordException() : base("Invalid P-assword !")
    {
    }
    
    public InvalidPAsswordException(string message) : base(message)
    {
    }

    
}