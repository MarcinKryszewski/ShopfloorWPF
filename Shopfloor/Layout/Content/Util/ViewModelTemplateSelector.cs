using System.Windows;
using System.Windows.Controls;
using Shopfloor.Features.Actions.ActionCreate;
using Shopfloor.Features.Actions.ActionDetails;
using Shopfloor.Features.Actions.ActionEdit;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.Actions.ActionTransfer;
using Shopfloor.Features.Actions.TrainingsList;
using Shopfloor.Features.ChemicalSubstances.Dashboard;
using Shopfloor.Features.God;
using Shopfloor.Features.Personal.PersonalTraining;
using Shopfloor.Features.Responsibilities.MachineResponsibilities;
using Shopfloor.Features.Responsibilities.MachineResponsibilityEdit;
using Shopfloor.Features.Trainings.ListTrainee;
using Shopfloor.Features.Trainings.ListTrainer;
using Shopfloor.Features.Trainings.TrainingCreate;
using Shopfloor.Features.Trainings.TrainingDetails;
using Shopfloor.Features.Trainings.TrainingEdit;
using Shopfloor.Features.WorkInProgressFeature;

namespace Shopfloor.Layout.Content.Util
{
    public class ViewModelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? ActionCreateTemplate { get; set; }
        public DataTemplate? ActionDetailsTemplate { get; set; }
        public DataTemplate? ActionEditTemplate { get; set; }
        public DataTemplate? ActionListTemplate { get; set; }
        public DataTemplate? ActionTransferTemplate { get; set; }
        public DataTemplate? GodTemplate { get; set; }
        public DataTemplate? MachineResponsibilitiesTemplate { get; set; }
        public DataTemplate? MachineResponsibilityEditemplate { get; set; }
        public DataTemplate? PersonalTrainingTemplate { get; set; }
        public DataTemplate? TrainingCreateTemplate { get; set; }
        public DataTemplate? TrainingDetailsTemplate { get; set; }
        public DataTemplate? ListTraineeTemplate { get; set; }
        public DataTemplate? ListTrainerTemplate { get; set; }
        public DataTemplate? TrainingEditTemplate { get; set; }
        public DataTemplate? WorkInProgressTemplate { get; set; }
        public DataTemplate? ChemicalSubstancesDashboardTemplate { get; set; }
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
                PersonalTrainingViewModel => PersonalTrainingTemplate,
                MachineResponsibilitiesViewModel => MachineResponsibilitiesTemplate,
                MachineResponsibilityEditViewModel => MachineResponsibilityEditemplate,
                TrainingCreateViewModel => TrainingCreateTemplate,
                TrainingDetailsViewModel => TrainingDetailsTemplate,
                ListTraineeViewModel => ListTraineeTemplate,
                ListTrainerViewModel => ListTrainerTemplate,
                TrainingEditViewModel => TrainingEditTemplate,
                ChemicalSubstancesDashboardViewModel => ChemicalSubstancesDashboardTemplate,
                _ => WorkInProgressTemplate,
            };
        }
    }
}