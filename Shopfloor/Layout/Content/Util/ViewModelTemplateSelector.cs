using System.Windows;
using System.Windows.Controls;
using Shopfloor.Features.Actions.ActionCreate;
using Shopfloor.Features.Actions.ActionDetails;
using Shopfloor.Features.Actions.ActionEdit;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.Actions.ActionTransfer;
using Shopfloor.Features.Actions.TrainingsList;
using Shopfloor.Features.God;
using Shopfloor.Features.Personal.PersonalTraining;
using Shopfloor.Features.Responsibilities.MachineResponsibilities;
using Shopfloor.Features.Responsibilities.MachineResponsibilityEdit;
using Shopfloor.Features.Trainings.ActionTraining;
using Shopfloor.Features.Trainings.PersonTraining;
using Shopfloor.Features.Trainings.TrainingDetails;
using Shopfloor.Features.Trainings.TrainingEdit;
using Shopfloor.Features.Trainings.TrainingMain;
using Shopfloor.Features.WorkInProgressFeature;

namespace Shopfloor.Layout.Content.Util
{
    public class ViewModelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? ActionCreateTemplate { get; set; }
        public DataTemplate? ActionDetailsTemplate { get; set; }
        public DataTemplate? ActionEditTemplate { get; set; }
        public DataTemplate? ActionListTemplate { get; set; }
        public DataTemplate? TrainingMainViewModelTemplate { get; set; }
        public DataTemplate? ActionTransferTemplate { get; set; }
        public DataTemplate? GodTemplate { get; set; }
        public DataTemplate? MachineResponsibilitiesTemplate { get; set; }
        public DataTemplate? MachineResponsibilityEditemplate { get; set; }
        public DataTemplate? PersonalTrainingTemplate { get; set; }
        public DataTemplate? TrainingCreateTemplate { get; set; }
        public DataTemplate? TrainingDetailsTemplate { get; set; }
        public DataTemplate? TrainingsListTemplate { get; set; }
        public DataTemplate? TrainingEditTemplate { get; set; }
        public DataTemplate? WorkInProgressTemplate { get; set; }
        public DataTemplate? ActionTrainingTemplate { get; set; }
        public DataTemplate? PersonTrainingTemplate { get; set; }
        public override DataTemplate? SelectTemplate(object item, DependencyObject? container)
        {
            return item switch
            {
                GodViewModel => GodTemplate,
                WorkInProgressViewModel => WorkInProgressTemplate,
                ActionCreateViewModel => ActionCreateTemplate,
                ActionDetailsViewModel => ActionDetailsTemplate,
                ActionEditViewModel => ActionEditTemplate,
                ActionsListViewModel => ActionListTemplate,
                ActionTransferViewModel => ActionTransferTemplate,
                TrainingsListViewModel => TrainingsListTemplate,
                PersonalTrainingViewModel => PersonalTrainingTemplate,
                MachineResponsibilitiesViewModel => MachineResponsibilitiesTemplate,
                MachineResponsibilityEditViewModel => MachineResponsibilityEditemplate,
                TrainingMainViewModel => TrainingMainViewModelTemplate,
                TrainingDetailsViewModel => TrainingDetailsTemplate,
                TrainingEditViewModel => TrainingEditTemplate,
                ActionTrainingView => ActionTrainingTemplate,
                PersonTrainingView => PersonTrainingTemplate,
                _ => WorkInProgressTemplate,
            };
        }
    }
}