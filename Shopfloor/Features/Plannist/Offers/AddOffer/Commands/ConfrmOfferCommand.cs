using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Stores;
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
            tasks.Add(UpdateErrandPartStatus(request));
            tasks.Add(ErrandPartNewStatus(request));
            tasks.Add(SelfConfirm(parameter, request));
            Task.WhenAll(tasks);
            ReturnToOffer(); //return or lock?
        }
        private async Task SelfConfirm(object? parameter, ErrandPart request)
        {
            if (parameter is null) return;
            if (parameter is not string) return;
            if ((string)parameter != "CONFIRM") return;

            await NewStatus(request, 2, null, "ZATWIERDZONO PODCZAS OFERTOWANIA");
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
        private async Task UpdateErrandPartStatus(ErrandPart request)
        {
            ErrandPartStatusProvider provider = _database.GetRequiredService<ErrandPartStatusProvider>();
            string comment = request.LastStatus.Comment ?? string.Empty;

            await provider.SetComment((int)request.Id!, comment);
        }
        private async Task ErrandPartNewStatus(ErrandPart request) => await NewStatus(request, 1, request.LastStatus.Comment, "DODANO OFERTĘ");

        private async Task NewStatus(ErrandPart request, int statusId, string? comment, string reason)
        {
            if (_currentUser.Id is null) return;
            ErrandPartStatusProvider errandPartStatusProvider = _database.GetRequiredService<ErrandPartStatusProvider>();
            ErrandPartStatus status = new(statusId)
            {
                Reason = reason,
                Comment = comment,
                ErrandPartId = (int)request.Id!,
                CreatedById = (int)_currentUser.Id,
                CreatedDate = DateTime.Now,
            };

            ErrandPartStatusStore store = _database.GetRequiredService<ErrandPartStatusStore>();
            store.Data.Add(status);

            await errandPartStatusProvider.Create(status);
        }

        private void ReturnToOffer()
        {
            _services.GetRequiredService<Notifier>().ShowSuccess("Dodano ofertę i przekazano do zatwierdzenia!");
            NavigationService<OffersViewModel> navigationService = _services.GetRequiredService<NavigationService<OffersViewModel>>();
            navigationService.Navigate();
        }
    }
}