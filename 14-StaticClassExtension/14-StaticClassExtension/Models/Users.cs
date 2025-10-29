namespace _14_StaticClassExtension.Models;

public class Users
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsLocked { get; set; }
    public int FailedAttempts { get; set; }

    public Users(string userName, string password, bool isLocked=false, int failedAttempts=0 )
    {
        
    }
}