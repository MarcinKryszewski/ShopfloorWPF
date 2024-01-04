using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Parts.Commands
{
    public class PartTypeCreateCommand : AsyncCommandBase
    {
        private readonly PartTypeProvider _provider;
        private readonly PartType _partType;

        public PartTypeCreateCommand(PartTypeProvider provider, PartType partType)
        {
            _provider = provider;
            _partType = partType;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            //PartType partType = new((string)parameter);
            await _partType.Add(_provider);
        }
    }
}