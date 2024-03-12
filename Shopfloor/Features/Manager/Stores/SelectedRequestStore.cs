using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Features.Manager.Stores
{
    internal sealed class SelectedRequestStore
    {
        public ErrandPart? Request { get; set; }
    }
}