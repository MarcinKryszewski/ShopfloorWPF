using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Parts
{
    internal class PartCreationModel : ModelValidationBase, IModelCreationModel<PartModel>
    {
        public PartModel CreateModel(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}