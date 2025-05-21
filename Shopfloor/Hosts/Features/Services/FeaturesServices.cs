using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Actions.ActionCreate;
using Shopfloor.Features.Actions.ActionDetails;
using Shopfloor.Features.Actions.ActionEdit;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.Actions.ActionsList.Utilities;
using Shopfloor.Features.Actions.ActionTransfer;
using Shopfloor.Features.Actions.TrainingsList;
using Shopfloor.Features.Personal.PersonalTraining;
using Shopfloor.Features.Responsibilities.MachineResponsibilities;
using Shopfloor.Features.Responsibilities.MachineResponsibilityEdit;
using Shopfloor.Features.Trainings.ListTrainee;
using Shopfloor.Features.Trainings.ListTrainer;
using Shopfloor.Features.Trainings.TrainingCreate;
using Shopfloor.Features.Trainings.TrainingDetails;
using Shopfloor.Features.Trainings.TrainingEdit;

namespace Shopfloor.Hosts.Features.Services
{
    internal static class FeaturesServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ActionsListViewModel>();
            services.AddTransient<ActionsFilter>();

            services.AddTransient<ActionDetailsViewModel>();
            services.AddTransient<ActionEditViewModel>();
            services.AddTransient<ActionCreateViewModel>();
            services.AddTransient<ActionTransferViewModel>();

            services.AddTransient<TrainingsListViewModel>();

            services.AddTransient<MachineResponsibilitiesViewModel>();
            services.AddTransient<MachineResponsibilityEditViewModel>();

            services.AddTransient<PersonalTrainingViewModel>();

            services.AddTransient<ListTraineeViewModel>();
            services.AddTransient<ListTrainerViewModel>();
            services.AddTransient<TrainingCreateViewModel>();
            services.AddTransient<TrainingDetailsViewModel>();
            services.AddTransient<TrainingEditViewModel>();
        }
    }
}