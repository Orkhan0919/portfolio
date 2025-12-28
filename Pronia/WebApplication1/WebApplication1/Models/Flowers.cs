using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Flowers : BaseEntity
{
    [Required]
    public string Title1 { get; set; }

    [Required]
    public string Title2 { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    [Required]
    public string ButtonLink { get; set; }

    [NotMapped]
    [Required]
    public IFormFile Photo { get; set; }
}


