namespace Shopfloor.Models.PartModel
{
    internal sealed class PartStore : StoreBase<Part>
    {
        public PartStore(PartProvider provider) : base(provider)
        {
        }
    }
}