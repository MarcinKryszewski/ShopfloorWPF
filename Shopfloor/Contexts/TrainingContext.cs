using System.Collections.Generic;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Persons;
using Shopfloor.Shared;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Contexts
{
    internal class TrainingContext : ObservableObject
    {
        public List<Person> Students { get; } = [];
        public Person? Trainer { get; set; }
        public Line? Line { get; set; }
        public Machine? Machine { get; set; }
        public Activity? Activity { get; set; }
        public ViewModelBase? TrainingList { get; set; }
    }
}