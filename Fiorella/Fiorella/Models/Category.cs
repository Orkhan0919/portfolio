using System.ComponentModel.DataAnnotations;
using Fiorella.Models.Base;

namespace Fiorella.Models;

public class Category : BaseEntity
{
    [MaxLength(30, ErrorMessage ="Cannot be longer than 30 characters")]
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
}