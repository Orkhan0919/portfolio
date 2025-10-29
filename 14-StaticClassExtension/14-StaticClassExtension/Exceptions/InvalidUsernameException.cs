namespace _14_StaticClassExtension.Models;

public class InvalidUsernameException : Exception
{
    public InvalidUsernameException() : base("Invalid username ! ")
    {
    }
    
    public InvalidUsernameException(string message) : base(message)
    {
    }
    
    
}