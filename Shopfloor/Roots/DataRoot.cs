using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivitiesInstructions;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Instructions;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.MachinesResponsibles;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Roots
{
    internal class DataRoot : IDataRoot
    {
        private readonly IRepository<Activity, ActivityCreation> _activities;
        private readonly IRepository<ActivityInstruction, ActivityInstructionCreation> _activitiesInstructions;
        private readonly IRepository<ActivityType, ActivityTypeCreation> _activityTypes;
        private readonly IRepository<Instruction, InstructionCreation> _instructions;
        private readonly IRepository<Line, LineCreation> _lines;
        private readonly IRepository<Machine, MachineCreation> _machines;
        private readonly IRepository<Person, PersonCreation> _persons;
        private readonly IRepository<MachineResponsible, MachineResponsibleCreation> _responsibles;
        private readonly IRepository<Training, TrainingCreation> _trainings;
        private readonly IRepository<Workshop, WorkshopCreation> _workshops;
        public DataRoot(
            IRepository<Activity, ActivityCreation> activities,
            IRepository<ActivityInstruction, ActivityInstructionCreation> activitiesInstructions,
            IRepository<ActivityType, ActivityTypeCreation> activityTypes,
            IRepository<Instruction, InstructionCreation> instructions,
            IRepository<Line, LineCreation> lines,
            IRepository<Machine, MachineCreation> machines,
            IRepository<Person, PersonCreation> persons,
            IRepository<MachineResponsible, MachineResponsibleCreation> responsibilitiesData,
            IRepository<Training, TrainingCreation> trainings,
            IRepository<Workshop, WorkshopCreation> workshops)
        {
            _activities = activities;
            _activitiesInstructions = activitiesInstructions;
            _activityTypes = activityTypes;
            _instructions = instructions;
            _lines = lines;
            _machines = machines;
            _persons = persons;
            _responsibles = responsibilitiesData;
            _trainings = trainings;
            _workshops = workshops;
        }
        public event EventHandler? DataChanged
        {
            add { throw new NotSupportedException(); }
            remove { throw new NotSupportedException(); }
        }
        public async Task<IEnumerable<Activity>> GetActivities() => await _activities.GetDataAsync();
        public async Task<IEnumerable<ActivityInstruction>> GetActivityInstructions() => await _activitiesInstructions.GetDataAsync();
        public async Task<IEnumerable<ActivityType>> GetActivityTypes() => await _activityTypes.GetDataAsync();
        public async Task<IEnumerable<Instruction>> GetInstructions() => await _instructions.GetDataAsync();
        public async Task<IEnumerable<Line>> GetLines() => await _lines.GetDataAsync();
        public async Task<IEnumerable<Machine>> GetMachines() => await _machines.GetDataAsync();
        public async Task<IEnumerable<Person>> GetPersons() => await _persons.GetDataAsync();
        public async Task<IEnumerable<MachineResponsible>> GetResponsibles() => await _responsibles.GetDataAsync();
        public async Task<IEnumerable<Training>> GetTrainings() => await _trainings.GetDataAsync();
        public async Task<IEnumerable<Workshop>> GetWorkshops() => await _workshops.GetDataAsync();
    }
}