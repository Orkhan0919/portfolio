using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Category : BaseEntity
{
    [Required(ErrorMessage = "Category name cannot be empty!")]
    [StringLength(30, ErrorMessage = "Category name cannot exceed 30 characters!")]

    public string Name { get; set; }
    public List<Product> Products { get; set; }
}