using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Identity;

public class RegisterVM
{

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required] [Compare("Password")] 
    public string ConfirmPassword { get; set; }
    
    [Required]
    public string Name { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Surname { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

}