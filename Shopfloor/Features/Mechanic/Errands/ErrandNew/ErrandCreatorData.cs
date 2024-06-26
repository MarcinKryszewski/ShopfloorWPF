using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using System.Collections.Generic;

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed class ErrandCreatorData
    {
        public required Errand Errand { get; set; }
        public List<ErrandPart> Parts { get; set; } = [];
        public int UserId { get; set; }
    }
}