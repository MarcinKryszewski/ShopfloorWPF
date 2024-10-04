using System.Windows;

namespace Shopfloor.Features.PartsList.Interfaces
{
    internal interface IViewModelContainingPartsList
    {
        public Visibility IsPartsListVisible { get; set; }
    }
}