using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.Interfaces;
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
        public ActivitiesRoot(
            IRepository<Activity, ActivityCreation> activityData,
            IRepository<Machine, MachineCreation> machineData,
            IRepository<Workshop, WorkshopCreation> workshopData,
            IRepository<ActivityType, ActivityTypeCreation> typeData)
        {
            _activityData = activityData;
            _machineData = machineData;
            _typeData = typeData;
            _workshopData = workshopData;
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
            IEnumerable<Machine> machines = await _machineData.GetDataAsync();

            foreach (Activity activity in activities)
            {
                await Task.Delay(400);
                activity.Machine = machines.FirstOrDefault(x => activity.MachineId == x.Id);
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
    }
}