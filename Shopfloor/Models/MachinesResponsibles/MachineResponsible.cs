using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.MachinesResponsibles
{
    internal class MachineResponsible : IModel
    {
        required public int Id { get; init; }
        public int MachineId { get; init; }
        public string Name => throw new NotSupportedException();
        public int PersonId { get; init; }
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
        }
    }
}