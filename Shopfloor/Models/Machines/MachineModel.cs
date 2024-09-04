using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.Machines
{
    public class MachineModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
    }
}