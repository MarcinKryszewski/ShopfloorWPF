using System.Collections.Generic;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Manufacturers;
using Shopfloor.Models.PartTypes;

namespace Shopfloor.Models.Parts
{
    internal class PartModel : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = "szt.";

        public ManufacturerModel? Manufacturer { get; set; }
        public int ManufacturerId { get; init; }

        public PartTypeModel? PartType { get; set; }
        public int PartTypeId { get; init; }

        public List<MachineModel> Machines { get; } = [];
        public string NameOriginal { get; set; } = string.Empty;
        public string NamePl { get; set; } = string.Empty;
        public string Index { get; init; } = string.Empty;
        public bool IsConfirmed { get; set; } = false;
    }
}