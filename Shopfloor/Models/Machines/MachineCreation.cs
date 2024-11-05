using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Machines
{
    internal class MachineCreation : ModelValidationBase, IModelCreationModel<Machine>
    {
        public int Id { get; set; }
        public Line? Line { get; set; }
        public int LineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Person? Responsible { get; set; }
        public int ResponsibleId { get; set; }
        public Machine CreateModel(int id)
        {
            return new Machine
            {
                Id = id,
                Line = Line,
                LineId = LineId,
                Name = Name,
                Responsible = Responsible,
                ResponsibleId = ResponsibleId,
            };
        }
    }
}