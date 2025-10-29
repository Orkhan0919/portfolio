namespace _14_StaticClassExtension.Models;

public class AccountLockedException: Exception
{
    public AccountLockedException() : base("Account locked !")
    {
    }
    
    
}