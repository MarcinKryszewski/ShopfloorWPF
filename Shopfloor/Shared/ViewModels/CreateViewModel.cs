namespace Shopfloor.Shared.ViewModels
{
    internal delegate TViewModel CreateViewModel<out TViewModel>()
        where TViewModel : ViewModelBase;

    internal delegate TViewModel CreateViewModel<in TParameter, out TViewModel>(TParameter parameter)
        where TViewModel : ViewModelBase;
}