using Shopfloor.Models.PartTypeModel.Store.Combine;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed class PartTypeStore : StoreBase<PartType>
    {
        public PartTypeStore(PartTypeProvider provider, PartTypeCombiner combiner) : base(provider, combiner)
        {

        }
    }
}