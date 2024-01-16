using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class MachinePart
    {
        private Part _part;
        private double _amount;
        private string _unit;

        public Part Part => _part;
        public double Amount => _amount;
        public string Unit => _unit;

        public MachinePart(Part part, double amount = 1, string unit = "szt.")
        {
            _part = part;
            _amount = amount;
            _unit = unit;
        }
    }
}