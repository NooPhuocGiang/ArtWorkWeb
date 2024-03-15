using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTier.Models
{
    public partial class Order
    {
        [Key]
        public int IdArtwork { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public int IdOrder { get; set; }
        public int UserId { get; set; }
        public int ArtworkId { get; set; }
        public DateTime Date { get; set; }
     

      
        public virtual Artwork Artwork { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }

}
