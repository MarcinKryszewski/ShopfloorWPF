using System.Collections.Generic;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Shared;

namespace Shopfloor.Contexts
{
    internal class TrainingContext : ObservableObject
    {
        public List<Training> Trainings { get; set; } = [];
        public List<Person> PeopleToTrain { get; set; } = [];
    }
}