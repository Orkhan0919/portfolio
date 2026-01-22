using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
using Microsoft.AspNetCore.Http;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string SKU { get; set; }
    public int CategoryId { get; set; }
    public string PrimaryImg { get; set; }      
    public string SecondaryImg { get; set; }
    public Category? Category { get; set; }
    public List<ProductTag> ProductTags { get; set; } = new();
}