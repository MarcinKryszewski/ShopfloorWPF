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
using Shopfloor.Features.Trainings.TrainingMain;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Layout.Content.Util;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.Content
{
    internal sealed class ContentViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly TopPanelViewModel _topPanelViewModel;
        public ContentViewModel(TopPanelViewModel topPanelViewModel, INavigationStore navigationStore)
        {
            _topPanelViewModel = topPanelViewModel;

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        public static DataTemplateSelector TemplateSelector => new ViewModelTemplateSelector()
        {
            GodTemplate = GetDataTemplate<GodView>(),
            WorkInProgressTemplate = GetDataTemplate<WorkInProgressView>(),
            ActionCreateTemplate = GetDataTemplate<ActionCreateView>(),
            ActionDetailsTemplate = GetDataTemplate<ActionDetailsView>(),
            ActionEditTemplate = GetDataTemplate<ActionEditView>(),
            ActionListTemplate = GetDataTemplate<ActionsListView>(),
            ActionTransferTemplate = GetDataTemplate<ActionTransferView>(),
            TrainingsListTemplate = GetDataTemplate<TrainingsListView>(),
            PersonalTrainingTemplate = GetDataTemplate<PersonalTrainingView>(),
            MachineResponsibilitiesTemplate = GetDataTemplate<MachineResponsibilitiesView>(),
            MachineResponsibilityEditemplate = GetDataTemplate<MachineResponsibilityEditView>(),
            TrainingMainViewModelTemplate = GetDataTemplate<TrainingMainView>(),
            TrainingDetailsTemplate = GetDataTemplate<TrainingDetailsView>(),
            ActionTrainingTemplate = GetDataTemplate<ActionTrainingView>(),
            PersonTrainingTemplate = GetDataTemplate<PersonTrainingView>(),
        };
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        public TopPanelViewModel TopPanelViewModel => _topPanelViewModel;
        private static DataTemplate GetDataTemplate<TView>()
        where TView : UserControl
        {
            return new DataTemplate(typeof(TView))
            {
                VisualTree = new FrameworkElementFactory(typeof(TView)),
            };
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}