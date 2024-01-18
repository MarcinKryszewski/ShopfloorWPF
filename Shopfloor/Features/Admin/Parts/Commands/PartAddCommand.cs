using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Features.Admin.Parts.Add;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartAddCommand : CommandBase
    {
        private PartsAddViewModel _partsAddViewModel;
        private IServiceProvider _mainServices;

        public PartAddCommand(PartsAddViewModel partsAddViewModel, IServiceProvider mainServices)
        {
            _partsAddViewModel = partsAddViewModel;
            _mainServices = mainServices;
        }

        public override void Execute(object? parameter)
        {

        }
    }
}