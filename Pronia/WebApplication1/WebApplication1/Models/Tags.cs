using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models;

public class Tags
{
  [Key] 
    public int TagId { get; set; }

    [Required]
    public string TagName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}