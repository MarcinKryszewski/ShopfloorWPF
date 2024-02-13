using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    internal sealed class MachineSelectedCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            //if (parameter is not Machine) return;
            //Machine machine = (Machine)parameter;
            //System.Diagnostics.Debug.WriteLine(machine.Name);
        }
    }
}