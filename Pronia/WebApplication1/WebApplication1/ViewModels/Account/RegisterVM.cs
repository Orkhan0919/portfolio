using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models.ViewModels;

public class RegisterVM
{
    [Required(ErrorMessage = "You must enter a username.")]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "You must  enter a name.")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "You must  enter a surname.")]
    [Display(Name = "Surname")]
    public string Surname { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords didn't match !")]
    public string ConfirmPassword { get; set; }
}