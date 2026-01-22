using WebApplication1.ViewModels;

namespace WebApplication1.Models
{
    public class HomeVM
    {
        public List<Product> Products { get; set; }
        public List<Flowers> Flowers { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public List<CategoryItemVM> Categories { get; set; }
        public List<TagItemVM> Tags { get; set; }
    }
}