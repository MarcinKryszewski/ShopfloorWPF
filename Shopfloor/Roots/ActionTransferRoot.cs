using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Contexts;
using Shopfloor.Features.Actions.ActionTransfer.Utilities;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.MachinesResponsibles;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Workshops;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Roots
{
    internal class ActionTransferRoot : IRoot
    {
        private readonly IDataRoot _data;
        private readonly ActivityContext _context;
        private readonly IRepository<Activity, ActivityCreation> _activityData;
        public ActionTransferRoot(
            IDataRoot data,
            ActivityContext context,
            IRepository<Activity, ActivityCreation> activityData)
        {
            _data = data;
            _context = context;
            _activityData = activityData;
        }
        public event EventHandler? DataChanged;
        public ConcurrentObservableCollection<Workshop> Workshops { get; private set; } = [];
        public ConcurrentObservableCollection<ResponsibleTraining> TrainingList { get; private set; } = [];
        public async Task LoadData()
        {
            List<Task> tasks = [];

            tasks.Add(LoadWorkshops());
            tasks.Add(LoadTrainingList());

            await Task.WhenAll(tasks);
            OnDataChanged();
        }
        public async Task TransferAction(Activity? activity, Workshop workshop)
        {
            ArgumentNullException.ThrowIfNull(activity);

            activity.Workshop = workshop;
            await _activityData.Update(ActivityMapper.ToCreationModel(activity));
        }
        protected void OnDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);
        private async Task LoadWorkshops()
        {
            Workshops.Clear();
            Workshops.InsertRange(0, await _data.GetWorkshops());
        }
        private async Task LoadTrainingList()
        {
            if (_context.Activity is null)
            {
                return;
            }

            List<Task> tasks = [];

            Task<IEnumerable<MachineResponsible>> responsiblesTask = LoadResponsiblesForMachine(_context.Activity.MachineId);
            Task<List<Person>> loadPersons = LoadPersons();

            await Task.WhenAll(tasks);

            IEnumerable<MachineResponsible> responsibles = await responsiblesTask;
            List<Person> persons = await loadPersons;
        }
        private async Task<IEnumerable<MachineResponsible>> LoadResponsiblesForMachine(int machineId)
        {
            IEnumerable<MachineResponsible> data = await _data.GetResponsibles();
            return data.Where(x => x.MachineId == machineId);
        }
        private async Task<List<Person>> LoadPersons()
        {
            return [.. await _data.GetPersons()];
        }
    }
}