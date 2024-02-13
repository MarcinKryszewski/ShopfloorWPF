using Shopfloor.Shared.ViewModels;
using System.Windows.Input;

namespace Shopfloor.Shared.Commands
{
    internal delegate ICommand CreateCommand<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase;
}