using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Shopfloor.Features.Admin.PartTypes.List
{
    public class PartTypesListViewModel : ViewModelBase, IInputForm<PartType>
    {
        private string _errorMassage = string.Empty;
        private readonly ObservableCollection<PartType> _partTypes = new();

        public string ErrorMassage
        {
            get => string.IsNullOrEmpty(_errorMassage) ? string.Empty : _errorMassage;
            set
            {
                _errorMassage = value;
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
            }
        }
        public Visibility HasErrorVisibility => string.IsNullOrEmpty(ErrorMassage) ? Visibility.Collapsed : Visibility.Visible;
        public ICollectionView PartTypes { get; }

        public PartTypesListViewModel(IServiceProvider databaseServices)
        {
        }



        public void CleanForm()
        {
            throw new System.NotImplementedException();
        }

        public bool IsDataValidate(PartType inputValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
