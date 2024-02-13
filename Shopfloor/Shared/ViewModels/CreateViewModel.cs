namespace Shopfloor.Shared.ViewModels
{
    internal delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    internal delegate TViewModel CreateViewModel<TParameter, TViewModel>(TParameter parameter) where TViewModel : ViewModelBase;
}