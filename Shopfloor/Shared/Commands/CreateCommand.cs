using System.Windows.Input;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Commands
{
    internal delegate ICommand CreateCommand<in TViewModel>(TViewModel viewModel)
        where TViewModel : ViewModelBase;
}