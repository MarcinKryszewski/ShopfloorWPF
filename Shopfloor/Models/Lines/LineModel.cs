using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Machines;

namespace Shopfloor.Models.Lines
{
    public class LineModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public List<MachineModel> Machines { get; } = [];
    }
}