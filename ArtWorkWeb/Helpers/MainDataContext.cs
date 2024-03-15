using DataTier.Models;
using Eatera.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Eatera.Helpers
{
    public class MainDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public MainDataContext(DbContextOptions<MainDataContext> options)
        : base(options)
        {
        }



        //public DbSet<User> Users { get; set; }
        public DbSet<Artwork> Arts { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
