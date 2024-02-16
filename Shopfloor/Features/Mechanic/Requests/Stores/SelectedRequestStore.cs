using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Features.Mechanic.Requests.Stores
{
    internal sealed class SelectedRequestStore
    {
        public ErrandPart? Request { get; set; }
    }
}