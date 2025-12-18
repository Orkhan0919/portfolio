using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category : BaseEntity
    { 
        [Required(ErrorMessage = "Ad mütləq yazılmalıdır!")]
        [StringLength(20, ErrorMessage = "Ad 20 simvoldan uzun ola bilməz!")]
        public string? Name { get; set; }
        public string? CreatedAt { get; set; } 
        public List<Product> Products { get; set; }
    }
}