using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Database.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public string ImagePath { get; set; } = String.Empty;
    }
}