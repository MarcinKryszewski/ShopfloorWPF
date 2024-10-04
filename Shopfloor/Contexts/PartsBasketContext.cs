using System.Collections.ObjectModel;
using Shopfloor.Models.Parts;

namespace Shopfloor.Contexts
{
    internal class PartsBasketContext
    {
        public ObservableCollection<PartBasketModel> Parts { get; } = [];
    }
    internal class PartBasketModel
    {
        required public PartModel Part { get; init; }
        public double Amount { get; set; } = 0;
    }
}