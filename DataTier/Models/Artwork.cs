using System;
using System.Collections.Generic;

namespace DataTier.Models
{
    public partial class Artwork
    {
        public Artwork()
        {
            ImageLists = new HashSet<ImageList>();
            Likes = new HashSet<Like>();
            Orders = new HashSet<Order>();
            PreOrders = new HashSet<PreOrder>();
        }

        public int IdArtwork { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Owner { get; set; }
        public string? Status { get; set; }
        public string? CategoryName { get; set; }
        public string? Author { get; set; }
        public string Description { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CompletionDate { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }




        public virtual User? OwnerNavigation { get; set; }
        public virtual ICollection<ImageList> ImageLists { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PreOrder> PreOrders { get; set; }
    }
}
