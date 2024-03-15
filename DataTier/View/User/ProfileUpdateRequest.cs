using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.View.User
{
    public class ProfileUpdateRequest
    {
        public int IdUser { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

    }
}
