using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Features.Admin.Parts.Interfaces;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartCleanFormCommand : CommandBase
    {
        private IPartForm _viewModel;

        public PartCleanFormCommand(IPartForm viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.CleanForm();
        }
    }
}