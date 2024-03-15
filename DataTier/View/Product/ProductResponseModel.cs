using DataTier.Models;

namespace DataTier.View.Product
{
    public class ProductResponseModel
    {
        public int IdArtwork { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Owner { get; set; }
        public string? Status { get; set; }
        public string? CategoryName { get; set; }
        public string? Author { get; set; }

        public virtual Models.User? OwnerNavigation { get; set; }
        public virtual ICollection<ImageList> ImageLists { get; set; }

    }
}
