using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Machines
{
    public class MachineModel : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
    }
}