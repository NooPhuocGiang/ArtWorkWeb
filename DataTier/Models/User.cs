using System;
using System.Collections.Generic;

namespace DataTier.Models
{
    public partial class User
    {
        public User()
        {
            Artworks = new HashSet<Artwork>();
            Likes = new HashSet<Like>();
            Orders = new HashSet<Order>();
            PreOrders = new HashSet<PreOrder>();
            Transactions = new HashSet<Transaction>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Artwork> Artworks { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PreOrder> PreOrders { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
