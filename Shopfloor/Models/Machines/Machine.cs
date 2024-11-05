using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Machines
{
    internal class Machine : IModel
    {
        required public int Id { get; init; }
        public Line? Line { get; set; }
        required public int LineId { get; init; }
        public string Name { get; set; } = string.Empty;
        public Person? Responsible { get; set; }
        public int ResponsibleId { get; set; }
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            if (data is not MachineCreation)
            {
                return;
            }

            MachineCreation creation = (MachineCreation)data;

            Name = creation.Name;
            Line = creation.Line;
            Responsible = creation.Responsible;
            ResponsibleId = creation.ResponsibleId;
        }
    }
}