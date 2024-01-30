using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private ErrandsNewViewModel _viewModel;
        private ErrandProvider _errandProvider;
        private int _creatorId;

        public ErrandNewCommand(ErrandsNewViewModel errandsNewViewModel, IServiceProvider databaseServices, int? creatorId)
        {
            _viewModel = errandsNewViewModel;
            _errandProvider = databaseServices.GetRequiredService<ErrandProvider>();
            _creatorId = creatorId ?? 0;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.IsDataValidate)
            {
                int machineId = _viewModel.SelectedMachine?.Id ?? 0;
                int typeId = _viewModel.SelectedType?.Id ?? 0;
                Errand errand = new(DateTime.Now, _creatorId, machineId, typeId, _viewModel.TaskDescription, _viewModel.SelectedPriority);
                if (_viewModel.SelectedDate is not null) errand.ExpectedDate = _viewModel.SelectedDate.Value;
                if (_viewModel.SelectedResponsible is not null) errand.Responsible = _viewModel.SelectedResponsible;
                errand.SapNumber = _viewModel.SapNumber;
                int errandId = _errandProvider.Create(errand).Result;
            }
        }
    }
}