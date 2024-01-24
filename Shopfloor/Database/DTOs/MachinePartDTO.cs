using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Database.DTOs
{
    public class MachinePartDTO
    {
        public int Machine_Id { get; set; }
        public int Part_Id { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; } = "szt.";
    }
}