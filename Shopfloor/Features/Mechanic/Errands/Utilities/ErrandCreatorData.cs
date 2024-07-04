using System.Collections.Generic;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed class ErrandCreatorData
    {
        required public Errand Errand { get; set; }
        public List<ErrandPart> Parts { get; set; } = [];
        public int UserId { get; set; }
    }
}