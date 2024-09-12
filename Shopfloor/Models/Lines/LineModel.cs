using System.Collections.Generic;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;

namespace Shopfloor.Models.Lines
{
    public class LineModel : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public List<MachineModel> Machines { get; } = [];
    }
}