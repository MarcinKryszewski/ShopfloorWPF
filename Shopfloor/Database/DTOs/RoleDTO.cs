using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Database.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}