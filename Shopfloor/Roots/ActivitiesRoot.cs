using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivitiesInstructions;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Roots
{
    internal class ActivitiesRoot : IRoot
    {
        private readonly IRepository<Activity, ActivityCreation> _activityData;
        private readonly IRepository<Machine, MachineCreation> _machineData;
        private readonly IRepository<Workshop, WorkshopCreation> _workshopData;
        private readonly IRepository<ActivityType, ActivityTypeCreation> _typeData;
        private readonly IRepository<Line, LineCreation> _lineData;
        private readonly IRepository<ActivityInstruction, ActivityInstructionCreation> _instructionsData;
        public ActivitiesRoot(
            IRepository<Activity, ActivityCreation> activityData,
            IRepository<Machine, MachineCreation> machineData,
            IRepository<Workshop, WorkshopCreation> workshopData,
            IRepository<ActivityType, ActivityTypeCreation> typeData,
            IRepository<Line, LineCreation> lineData,
            IRepository<ActivityInstruction, ActivityInstructionCreation> instructionsData)
        {
            _activityData = activityData;
            _machineData = machineData;
            _typeData = typeData;
            _workshopData = workshopData;
            _lineData = lineData;
            _instructionsData = instructionsData;
        }
        public event EventHandler? DataChanged;
        public async Task<IEnumerable<Activity>> GetData()
        {
            IEnumerable<Activity> data = await _activityData.GetDataAsync();

            if (!_activityData.Merges.Contains(typeof(Machine)))
            {
                _ = DecorateWitMachines(data);
            }

            if (!_activityData.Merges.Contains(typeof(Workshop)))
            {
                _ = DecorateWitWorkshops(data);
            }

            if (!_activityData.Merges.Contains(typeof(ActivityType)))
            {
                _ = DecorateWitActivityTypes(data);
            }

            if (!_activityData.Merges.Contains(typeof(ActivityInstruction)))
            {
                _ = DecorateWitInstructions(data);
            }

            return await _activityData.GetDataAsync();
        }
        public async Task CreateActivity(ActivityCreation data)
        {
            await _activityData.Create(data);
            OnDataChanged(EventArgs.Empty);
        }
        public async Task UpdateActivity(ActivityCreation data)
        {
            await _activityData.Update(data);
            OnDataChanged(EventArgs.Empty);
        }
        public async Task DeleteActivity(Activity data)
        {
            await _activityData.Delete(data.Id);
            OnDataChanged(EventArgs.Empty);
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
        private async Task DecorateWitMachines(IEnumerable<Activity> activities)
        {
            Task<List<Machine>>? machineTask = _machineData.GetDataAsync();
            Task<List<Line>>? lineTask = _lineData.GetDataAsync();

            await Task.WhenAll(machineTask, lineTask);

            IEnumerable<Machine> machines = await machineTask;
            IEnumerable<Line> lines = await lineTask;

            foreach (Activity activity in activities)
            {
                Machine? machine = machines.FirstOrDefault(x => activity.MachineId == x.Id);
                if (machine != null)
                {
                    machine.Line = lines.FirstOrDefault(x => machine.LineId == x.Id);
                }
                activity.Machine = machine;
            }

            _activityData.Merges.Add(typeof(Machine));
            OnDataChanged(EventArgs.Empty);
        }
        private async Task DecorateWitWorkshops(IEnumerable<Activity> activities)
        {
            IEnumerable<Workshop> workshops = await _workshopData.GetDataAsync();

            foreach (Activity activity in activities)
            {
                activity.Workshop = workshops.FirstOrDefault(x => activity.WorkshopId == x.Id);
            }

            _activityData.Merges.Add(typeof(Workshop));
            OnDataChanged(EventArgs.Empty);
        }
        private async Task DecorateWitActivityTypes(IEnumerable<Activity> activities)
        {
            IEnumerable<ActivityType> types = await _typeData.GetDataAsync();

            foreach (Activity activity in activities)
            {
                activity.Type = types.FirstOrDefault(x => activity.TypeId == x.Id);
            }

            _activityData.Merges.Add(typeof(ActivityType));
            OnDataChanged(EventArgs.Empty);
        }
        private async Task DecorateWitInstructions(IEnumerable<Activity> activities)
        {
            IEnumerable<ActivityInstruction> instructions = await _instructionsData.GetDataAsync();

            foreach (Activity activity in activities)
            {
                activity.HasInstruction = instructions.Any(x => activity.Id == x.ActivityId);
            }

            _activityData.Merges.Add(typeof(ActivityInstruction));
            OnDataChanged(EventArgs.Empty);
        }
    }
}