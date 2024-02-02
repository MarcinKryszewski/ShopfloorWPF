using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Features.Mechanic.Errands.Stores
{
    public class SelectedErrandStore
    {
        public int? ErrandId { get; set; }
        public int? MachineId { get; set; }
        public List<ErrandPart> ErrandParts = [];
    }
}