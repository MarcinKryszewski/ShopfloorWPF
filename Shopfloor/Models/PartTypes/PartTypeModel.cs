using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.PartTypes
{
    internal class PartTypeModel : IModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
    }
}