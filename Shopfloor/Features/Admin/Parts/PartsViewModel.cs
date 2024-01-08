using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using System;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Parts
{
    public class PartsViewModel : ViewModelBase
    {
        private string _partTypeName = "";
        public PartType PartType => new(_partTypeName);

        public string PartTypeName
        {
            get => _partTypeName;
            set
            {
                _partTypeName = value;
                OnPropertyChanged(nameof(PartTypeName));
            }
        }

        public ICommand PartTypeCreateCommand { get; }

        public PartsViewModel(IServiceProvider databaseServices)
        {
            PartTypeCreateCommand = new PartTypeCreateCommand(databaseServices.GetRequiredService<PartTypeProvider>(), this);
        }
    }
}