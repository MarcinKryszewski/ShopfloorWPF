using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Errands.Interfaces
{
    public interface IPartsList
    {
        public ViewModelBase? PartsList { get; set; }
    }
}