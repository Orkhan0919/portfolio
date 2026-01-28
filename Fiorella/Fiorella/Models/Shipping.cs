using Fiorella.Models.Base;

namespace Fiorella.Models
{
    public class Shipping : BaseEntity
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string ImageURL { get; set; }
    }
}
