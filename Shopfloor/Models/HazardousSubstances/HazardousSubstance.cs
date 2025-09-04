using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardousSubstances
{
    internal class HazardousSubstance : IModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public double Nds { get; init; }
        public double Ndsch { get; init; }
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            throw new NotImplementedException();
        }
    }
}