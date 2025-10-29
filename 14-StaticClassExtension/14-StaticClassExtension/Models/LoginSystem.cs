namespace _14_StaticClassExtension.Models;

public class LoginSystem
{
    private int attemptsLeft = 3;
    private int failedAttempts = 0;
    string[] users = { "student", "teacher", "admin" };
    string[] passwords = { "student123", "teacher123", "admin123" };

    public void ValidateUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidUsernameException("Username can't be empty!");
        }
        if (username.Length <= 3)
        {
            throw new InvalidUsernameException("Username must be longer than 3 characters!");
        }

        int index = Array.IndexOf(users, username.ToLower());
        if (index == -1)
        {
            throw new InvalidUsernameException();
        }

    }

    public void ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new InvalidPAsswordException("Password can't be empty!");
        }
        if (password.Length <= 6)
        {
            throw new InvalidPAsswordException("Password can't be less than 6 characters!");
        }
    }

    private void FindUser(string username)
    {
        username = username.ToLower();
        bool found = false;
        for (int i = 0; i < users.Length; i++)
        {
            if (users[i] == username)
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new UserNotFoundException(username);
        }
    }

    private bool CheckPassword(string username, string password)
    {
        int userIndex = Array.IndexOf(users, username.ToLower());

        if (password != passwords[userIndex])
        {
            failedAttempts++;
            attemptsLeft--;
            return false;
        }

        failedAttempts = 0;
        return true;
    }

    private bool IsAccountLocked()
    {
        return attemptsLeft <= 0;
    }

    public void Login()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        ValidateUsername(username);
        FindUser(username);

        while (!IsAccountLocked())
        {
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            ValidatePassword(password);

            bool success = CheckPassword(username, password);

            if (success)
            {
                Console.WriteLine($"Login successful! Welcome {username}");
                break;
            }
            else if (IsAccountLocked())
            {
                Console.WriteLine("Account is locked!");
                break;
            }
            else
            {
                Console.WriteLine($"Incorrect password! Attempts left: {attemptsLeft}");
            }
        }
    }
}
