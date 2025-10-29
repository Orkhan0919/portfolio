namespace _14_StaticClassExtension.Models;

public class IncorrectPasswordException:Exception
{
    public int AttemptsLeft { get; } = 3;
   

    public IncorrectPasswordException(int attemptsLeft)
        : base($"Incorrect password! Attempts left: {attemptsLeft}")
    {
        AttemptsLeft = attemptsLeft;
    }
    public IncorrectPasswordException(string message)
        : base(message)
    {
    }

    

}
