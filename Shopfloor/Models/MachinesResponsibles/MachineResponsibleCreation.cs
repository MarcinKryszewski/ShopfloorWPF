using System;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.MachinesResponsibles
{
    internal class MachineResponsibleCreation : ModelValidationBase, IModelCreationModel<MachineResponsible>
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int PersonId { get; set; }
        public MachineResponsible CreateModel(int id)
        {
            return new MachineResponsible
            {
                Id = id,
                MachineId = MachineId,
                PersonId = PersonId,
            };
        }
    }
}