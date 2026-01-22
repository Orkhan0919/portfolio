namespace WebApplication1.ViewModels;

public class ProductUpdateVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string SKU { get; set; }
    public int CategoryId { get; set; }
    public string? ExistingPrimaryImg { get; set; } 
    public string? ExistingSecondaryImg { get; set; }
    public IFormFile? MainPhoto { get; set; } 
    public IFormFile? SecondaryPhoto { get; set; }
    public int[]? TagIds { get; set; }
}