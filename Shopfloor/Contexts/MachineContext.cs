using Shopfloor.Models.Machines;
using Shopfloor.Shared;

namespace Shopfloor.Contexts
{
    internal class MachineContext : ObservableObject
    {
        public Machine? Machine { get; set; }
    }
}