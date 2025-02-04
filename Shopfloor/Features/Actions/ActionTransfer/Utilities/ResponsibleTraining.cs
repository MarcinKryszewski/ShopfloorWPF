using Shopfloor.Models.Persons;

namespace Shopfloor.Features.Actions.ActionTransfer.Utilities
{
    internal class ResponsibleTraining
    {
        required public Person Responsible { get; set; }
        public TrainingStatus TrainingStatus { get; set; } = TrainingStatus.Untrained;
    }
}