using Shopfloor.Shared.ViewModels;
using System.Windows.Input;

namespace Shopfloor.Shared.Commands
{
    public delegate ICommand CreateCommand<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase;
}
