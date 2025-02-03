using System.Windows;
using System.Windows.Controls;
using Shopfloor.Features.ActionDetails;
using Shopfloor.Features.ActionEdit;
using Shopfloor.Features.Actions.ActionCreate;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.Actions.ActionTransfer;
using Shopfloor.Features.God;
using Shopfloor.Features.MachineResponsibilities;
using Shopfloor.Features.MachineResponsibilityEdit;
using Shopfloor.Features.Personal.PersonalTraining;
using Shopfloor.Features.TrainingFeatures.TrainingCreate;
using Shopfloor.Features.TrainingFeatures.TrainingDetails;
using Shopfloor.Features.Trainings.ActionTrainingList;
using Shopfloor.Features.TrainingsList;
using Shopfloor.Features.WorkInProgressFeature;

namespace Shopfloor.Layout.Content.Util
{
    public class ViewModelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? GodTemplate { get; set; }
        public DataTemplate? WorkInProgressTemplate { get; set; }
        public DataTemplate? ActionCreateTemplate { get; set; }
        public DataTemplate? ActionDetailsTemplate { get; set; }
        public DataTemplate? ActionEditTemplate { get; set; }
        public DataTemplate? ActionListTemplate { get; set; }
        public DataTemplate? ActionTransferTemplate { get; set; }
        public DataTemplate? TrainingsListTemplate { get; set; }
        public DataTemplate? PersonalTrainingTemplate { get; set; }
        public DataTemplate? MachineResponsibilitiesTemplate { get; set; }
        public DataTemplate? MachineResponsibilityEditemplate { get; set; }
        public DataTemplate? ActionTrainingListTemplate { get; set; }
        public DataTemplate? TrainingCreateTemplate { get; set; }
        public DataTemplate? TrainingDetailsTemplate { get; set; }
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
                ActionTrainingListViewModel => ActionTrainingListTemplate,
                TrainingCreateViewModel => TrainingCreateTemplate,
                TrainingDetailsViewModel => TrainingDetailsTemplate,
                _ => base.SelectTemplate(item, container),
            };
        }
    }
}