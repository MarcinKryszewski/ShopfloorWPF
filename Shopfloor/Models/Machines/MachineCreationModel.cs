using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Machines
{
    internal class MachineCreationModel : ModelValidationBase, IModelCreationModel<MachineModel>
    {
        public MachineModel CreateModel(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}