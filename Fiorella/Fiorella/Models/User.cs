using Microsoft.AspNetCore.Identity;

namespace Fiorella.Models;

public class User :IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    
}