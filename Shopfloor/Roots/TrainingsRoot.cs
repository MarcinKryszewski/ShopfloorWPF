using System;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Students;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Roots
{
    internal class TrainingsRoot : IRoot
    {
        private readonly IRepository<Training, TrainingCreation> _trainingRepository;
        private readonly IRepository<Student, StudentCreation> _studentRepository;
        private readonly IRepository<Person, PersonCreation> _personRepository;
        public TrainingsRoot(
            IRepository<Training, TrainingCreation> trainingRepository,
            IRepository<Student, StudentCreation> studentRepository,
            IRepository<Person, PersonCreation> personRepository)
        {
            _trainingRepository = trainingRepository;
            _studentRepository = studentRepository;
            _personRepository = personRepository;
        }
        public event EventHandler? DataChanged;
        protected void OnDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);
        public async Task LoadData()
        {

        }
    }
}