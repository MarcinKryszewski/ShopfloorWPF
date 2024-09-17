using System.Collections.Generic;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Parts;

namespace Shopfloor.Models.Machines
{
    internal class MachineModel : IModel
    {
        required public int Id { get; init; }

        public int LineId { get; init; }
        public LineModel? Line { get; set; }

        public string Name { get; set; } = string.Empty;
        public List<PartModel> Parts { get; } = [];
    }
}