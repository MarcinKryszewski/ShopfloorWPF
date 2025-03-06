using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Students;
using Shopfloor.Models.Trainings;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Roots
{
    internal class TrainingsRoot : IRoot
    {
        private readonly IRepository<Training, TrainingCreation> _trainingRepository;
        private readonly IRepository<Student, StudentCreation> _studentRepository;
        private readonly IRepository<Activity, ActivityCreation> _activityRepository;
        private readonly IRepository<Person, PersonCreation> _personRepository;
        public TrainingsRoot(
            IRepository<Training, TrainingCreation> trainingRepository,
            IRepository<Student, StudentCreation> studentRepository,
            IRepository<Activity, ActivityCreation> activityRepository,
            IRepository<Person, PersonCreation> personRepository)
        {
            _trainingRepository = trainingRepository;
            _studentRepository = studentRepository;
            _activityRepository = activityRepository;
            _personRepository = personRepository;
        }
        public event EventHandler? DataChanged;
        public ConcurrentObservableCollection<Training> Data { get; private set; } = [];
        public async Task GetData()
        {
            ICollection<Training> data = await _trainingRepository.GetDataAsync();

            List<Task> merges = [];
            if (!_trainingRepository.Merges.Contains(typeof(Person)))
            {
                merges.Add(DecorateWithPersons(data));
            }
            if (!_trainingRepository.Merges.Contains(typeof(Student)))
            {
                merges.Add(DecorateWithStudents(data));
            }
            if (!_trainingRepository.Merges.Contains(typeof(Activity)))
            {
                merges.Add(DecorateWithActivities(data));
            }
            await Task.WhenAll(merges);

            Data.Clear();
            await Task.Run(() => Parallel.ForEach(data, item =>
            {
                Data.Add(item);
            }));

            OnDataChanged();
        }
        protected void OnDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);
        private async Task DecorateWithStudents(ICollection<Training> data)
        {
            IEnumerable<Student> students = await _studentRepository.GetDataAsync();
            if (_studentRepository.Merges.Contains(typeof(Person)))
            {
                _ = Task.Run(() => DecorateStudents(students));
            }

            foreach (Training training in data)
            {
                training.Students.AddRange(students.Where(s => s.TrainingId == training.Id));
            }

            _trainingRepository.Merges.Add(typeof(Student));
        }
        private async Task DecorateWithPersons(ICollection<Training> data)
        {
            IEnumerable<Person> people = await _personRepository.GetDataAsync();

            foreach (Training training in data)
            {
                training.Teacher = people.FirstOrDefault(x => training.TeacherId == x.Id);
            }

            _trainingRepository.Merges.Add(typeof(Person));
        }
        private async Task DecorateWithActivities(ICollection<Training> data)
        {
            IEnumerable<Activity> activities = await _activityRepository.GetDataAsync();

            foreach (Training training in data)
            {
                training.Activity = activities.FirstOrDefault(x => training.ActivityId == x.Id);
            }

            _trainingRepository.Merges.Add(typeof(Activity));
        }
        private async Task DecorateStudents(IEnumerable<Student> data)
        {
            IEnumerable<Person> people = await _personRepository.GetDataAsync();

            foreach (Student student in data)
            {
                student.Person = people.FirstOrDefault(x => x.Id == student.PersonId);
            }

            _trainingRepository.Merges.Add(typeof(Person));
        }
    }
}