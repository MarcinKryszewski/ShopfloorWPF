using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Shopfloor.Features.Plannist.Offers.AddOffer
{
    internal sealed class ConfrmOfferCommand : CommandBase
    {
        private readonly SelectedRequestStore _requestStore;
        private readonly AddOfferViewModel _viewModel;
        private readonly IServiceProvider _database;
        private readonly IServiceProvider _services;
        private readonly User _currentUser;
        public ConfrmOfferCommand(SelectedRequestStore requestStore, AddOfferViewModel addOfferViewModel, IServiceProvider databaseServices, IServiceProvider userServices, IServiceProvider mainServices)
        {
            _requestStore = requestStore;
            _viewModel = addOfferViewModel;
            _database = databaseServices;
            _services = mainServices;
            _currentUser = userServices.GetRequiredService<CurrentUserStore>().User!;
        }
        public override void Execute(object? parameter)
        {
            if (_requestStore.Request is null) return;

            ErrandPart request = _requestStore.Request;
            if (!_viewModel.IsDataValidate) return;
            if (request.Id is null) return;

            List<Task> tasks = [];
            tasks.Add(UpdateErrandPart(request));
            tasks.Add(ErrandPartStatusConfirm(request.LastStatus));
            tasks.Add(AddNextStatus(request, parameter));
            Task.WhenAll(tasks);
            ReturnToOffer(); //return or lock?
        }

        private async Task AddNextStatus(ErrandPart request, object? parameter)
        {
            ErrandPartStatus newStatus = new(ErrandPartStatus.Status[1])
            {
                CreatedDate = DateTime.Now,
                Reason = "OFFER ADDED",
                ErrandPartId = (int)request.Id!
            };
            await ErrandPartStatusNew(newStatus);
            if (SelfConfirm(parameter))
            {
                List<Task> tasks = [];
                newStatus.Comment = "Potwierdzone podczas składania oferty";
                tasks.Add(ErrandPartStatusConfirm(newStatus));
                tasks.Add(ErrandPartStatusNew(new(ErrandPartStatus.Status[3])
                {
                    CreatedDate = DateTime.Now,
                    Reason = "CONFIRMED DURING OFFER",
                    ErrandPartId = (int)request.Id!
                }));
                await Task.WhenAll(tasks);
            }
        }
        private static bool SelfConfirm(object? parameter)
        {
            if (parameter is null) return false;
            if (parameter is not string) return false;
            if ((string)parameter == "CONFIRM") return true;
            return false;
        }
        private async Task UpdateErrandPart(ErrandPart request)
        {
            ErrandPartProvider errandPartProvider = _database.GetRequiredService<ErrandPartProvider>();
            double price = _viewModel.PricePerUnit;
            DateTime? deliveryDate = request.ExpectedDeliveryDate;
            request.SetPrice(price);
            await errandPartProvider.UpdatePrice((int)request.Id!, price);
            await errandPartProvider.UpdateDeliveryDate((int)request.Id!, deliveryDate);
        }
        private async Task ErrandPartStatusConfirm(ErrandPartStatus status)
        {
            ErrandPartStatusProvider provider = _database.GetRequiredService<ErrandPartStatusProvider>();
            await provider.ConfirmStatus((int)status.Id!, status.Comment, (int)_currentUser.Id!);
        }
        private async Task ErrandPartStatusNew(ErrandPartStatus status)
        {
            ErrandPartStatusProvider provider = _database.GetRequiredService<ErrandPartStatusProvider>();
            status.Id = await provider.Create(status);
            AddToStatusStore(status);
        }
        private void AddToStatusStore(ErrandPartStatus status)
        {
            ErrandPartStatusStore store = _database.GetRequiredService<ErrandPartStatusStore>();
            store.Data.Add(status);
        }
        private void ReturnToOffer()
        {
            _services.GetRequiredService<Notifier>().ShowSuccess("Dodano ofertę i przekazano do zatwierdzenia!");
            //NavigationService<OffersViewModel> navigationService = _services.GetRequiredService<NavigationService<OffersViewModel>>();
            //navigationService.Navigate();
        }
    }
}