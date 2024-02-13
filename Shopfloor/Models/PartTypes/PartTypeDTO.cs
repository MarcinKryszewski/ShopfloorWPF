using System;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed class PartTypeDTO
    {
        public int? Id { get; set; }
        public string Part_Type_Name { get; set; } = String.Empty;
    }
}