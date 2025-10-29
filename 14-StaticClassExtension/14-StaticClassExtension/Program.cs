using _14_StaticClassExtension.Models;

LoginSystem  login = new LoginSystem();
while (true)
{


    try
    {
        login.Login();
    }
    catch (InvalidUsernameException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (AccountLockedException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (IncorrectPasswordException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (UserNotFoundException ex)
    {
        Console.WriteLine(ex.Message);
    }

    catch (InvalidPAsswordException ex)
    {
        Console.WriteLine(ex.Message);
    }
    
}

