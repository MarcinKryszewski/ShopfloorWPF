using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class Line : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
    }
}