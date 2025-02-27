using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Roots
{
    internal class TrainingsRoot : IRoot
    {
        private readonly IRepository<Activity, ActivityCreation> _activityData;
        private readonly IRepository<Person, PersonCreation> _personData;
        private readonly IRepository<Training, TrainingCreation> _trainingData;
        public TrainingsRoot(
            IRepository<Activity, ActivityCreation> activityData,
            IRepository<Training, TrainingCreation> trainingData,
            IRepository<Person, PersonCreation> personData)
        {
            _activityData = activityData;
            _trainingData = trainingData;
            _personData = personData;
        }
        public event EventHandler? DataChanged;
        public async Task ConfirmTraining(TrainingCreation data)
        {
            await _trainingData.Update(data);
            OnDataChanged(EventArgs.Empty);
        }
        public async Task<IEnumerable<Training>> GetData()
        {
            IEnumerable<Training> data = await _trainingData.GetDataAsync();

            if (!_trainingData.Merges.Contains(typeof(Person)))
            {
                _ = DecorateWithPersons(data);
            }

            if (!_trainingData.Merges.Contains(typeof(Activity)))
            {
                _ = DecorateWithActivities(data);
            }

            return data;
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
        private async Task DecorateWithActivities(IEnumerable<Training> data)
        {
            IEnumerable<Activity> activities = await _activityData.GetDataAsync();

            foreach (Training item in data)
            {
                item.Activity = activities.FirstOrDefault(x => item.ActivityId == x.Id);
            }

            _trainingData.Merges.Add(typeof(Person));
            OnDataChanged(EventArgs.Empty);
        }
        private async Task DecorateWithPersons(IEnumerable<Training> data)
        {
            IEnumerable<Person> persons = await _personData.GetDataAsync();

            foreach (Training item in data)
            {
                item.Trainee = persons.FirstOrDefault(x => item.TraineeId == x.Id);
            }

            _trainingData.Merges.Add(typeof(Person));
            OnDataChanged(EventArgs.Empty);
        }
    }
}