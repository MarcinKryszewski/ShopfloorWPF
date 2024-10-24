﻿using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.PartTypes.Commands
{
    internal sealed class PartTypeEditCommand : CommandBase
    {
        private readonly IProvider<PartType> _provider;
        private readonly PartTypesListViewModel _viewModel;
        public PartTypeEditCommand(PartTypesListViewModel viewModel, IProvider<PartType> provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.SelectedPartType is null)
            {
                return;
            }

            PartType selectedPartType = _viewModel.SelectedPartType;
            if (selectedPartType.Id is null)
            {
                return;
            }

            PartType partType = new(
                (int)selectedPartType.Id,
                _viewModel.Name);

            _ = _provider.Update(partType);

            Task.Run(() => _viewModel.UpdateData(selectedPartType));

            _viewModel.CleanForm();
        }
    }
}