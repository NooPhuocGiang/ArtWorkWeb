using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatera.Models
{


    public class CreateArt
    {

        public string Name { get; set; }
        public DateTime CompletionDate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public String? Image { get; set; }
    }
}
