using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vapor.Models
{
    public class Role
    {
        public Role()
        {
            Users = new List<string>();
        }

        [Required]
        public string RoleName { get; set; }
        public string Id { get; set; }
        public List<string> Users { get; set; }
    }
}
