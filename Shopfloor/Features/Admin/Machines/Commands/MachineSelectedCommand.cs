using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    public class MachineSelectedCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            //if (parameter is not Machine) return;
            //Machine machine = (Machine)parameter;
            //System.Diagnostics.Debug.WriteLine(machine.Name);
        }
    }
}