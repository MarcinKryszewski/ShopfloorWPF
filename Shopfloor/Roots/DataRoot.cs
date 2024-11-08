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
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Roots
{
    internal class DataRoot : IRoot
    {
        private readonly IRepository<Activity, ActivityCreation> _activities;
        private readonly IRepository<ActivityInstruction, ActivityInstructionCreation> _activitiesInstructions;
        private readonly IRepository<ActivityType, ActivityTypeCreation> _activityTypes;
        private readonly IRepository<Instruction, InstructionCreation> _instructions;
        private readonly IRepository<Line, LineCreation> _lines;
        private readonly IRepository<Machine, MachineCreation> _machines;
        private readonly IRepository<Person, PersonCreation> _persons;
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
            IRepository<Training, TrainingCreation> trainings,
            IRepository<Workshop, WorkshopCreation> workshops
        )
        {
            _activities = activities;
            _activitiesInstructions = activitiesInstructions;
            _activityTypes = activityTypes;
            _instructions = instructions;
            _lines = lines;
            _machines = machines;
            _persons = persons;
            _trainings = trainings;
            _workshops = workshops;
        }

        public event EventHandler? DataChanged;

        public async Task<IEnumerable<Activity>> GetActivity() => await _activities.GetDataAsync();
        public async Task<IEnumerable<ActivityInstruction>> GetActivityInstruction() => await _activitiesInstructions.GetDataAsync();
        public async Task<IEnumerable<ActivityType>> GetActivityType() => await _activityTypes.GetDataAsync();
        public async Task<IEnumerable<Instruction>> GetInstruction() => await _instructions.GetDataAsync();
        public async Task<IEnumerable<Line>> GetLine() => await _lines.GetDataAsync();
        public async Task<IEnumerable<Machine>> GetMachine() => await _machines.GetDataAsync();
        public async Task<IEnumerable<Person>> GetPerson() => await _persons.GetDataAsync();
        public async Task<IEnumerable<Training>> GetTraining() => await _trainings.GetDataAsync();
        public async Task<IEnumerable<Workshop>> GetWorkshop() => await _workshops.GetDataAsync();
    }
}