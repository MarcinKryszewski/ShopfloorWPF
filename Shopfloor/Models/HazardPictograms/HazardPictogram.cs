using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardPictograms
{
    internal class HazardPictogram : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            throw new NotImplementedException();
        }
    }
}