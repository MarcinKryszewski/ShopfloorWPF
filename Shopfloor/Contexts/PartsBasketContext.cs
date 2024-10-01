using System.Collections.ObjectModel;
using Shopfloor.Models.Parts;

namespace Shopfloor.Contexts
{
    internal class PartsBasketContext
    {
        public ObservableCollection<PartModel> Parts { get; } = [];
    }
}