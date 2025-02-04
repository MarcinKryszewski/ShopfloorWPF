using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivitiesInstructions;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Instructions;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.MachinesResponsibles;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Roots
{
    internal interface IDataRoot : IRoot
    {
        public Task<IEnumerable<Activity>> GetActivities();
        public Task<IEnumerable<ActivityInstruction>> GetActivityInstructions();
        public Task<IEnumerable<ActivityType>> GetActivityTypes();
        public Task<IEnumerable<Instruction>> GetInstructions();
        public Task<IEnumerable<Line>> GetLines();
        public Task<IEnumerable<Machine>> GetMachines();
        public Task<IEnumerable<Person>> GetPersons();
        public Task<IEnumerable<MachineResponsible>> GetResponsibles();
        public Task<IEnumerable<Training>> GetTrainings();
        public Task<IEnumerable<Workshop>> GetWorkshops();
    }
}