using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SubstanceHazards
{
    internal class SubstanceHazard : IModel
    {
        required public int Id { get; init; }
        public int SubstanceId { get; init; }
        public int HazardId { get; init; }
        public string Name => throw new NotSupportedException();
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            throw new NotImplementedException();
        }
    }
}