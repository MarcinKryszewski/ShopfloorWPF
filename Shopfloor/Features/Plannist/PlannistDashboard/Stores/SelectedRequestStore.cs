using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Features.Plannist.PlannistDashboard.Stores
{
    internal sealed class SelectedRequestStore
    {
        public ErrandPart? Request { get; set; }
    }
}