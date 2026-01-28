using System.ComponentModel.DataAnnotations.Schema;
using Fiorella.Models.Base;

namespace Fiorella.Models;

public class Slider : BaseEntity
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public string? ImageURL { get; set; }
    public int Order { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }
}