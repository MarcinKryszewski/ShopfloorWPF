using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;

namespace Shopfloor.Features.Plannist.Offers.AddOffer
{
    internal sealed class ConfrmOfferCommand : CommandBase
    {
        private readonly User _currentUser;
        private readonly ErrandPartProvider _errandPartProvider;
        private readonly ErrandPartStatusProvider _errandPartStatusProvider;
        private readonly IDataStore<ErrandPartStatus> _errandPartStatusStore;
        private readonly INotifier _notifier;
        private readonly SelectedRequestStore _requestStore;
        private readonly AddOfferViewModel _viewModel;
        public ConfrmOfferCommand(
            SelectedRequestStore requestStore,
            AddOfferViewModel addOfferViewModel,
            ICurrentUserStore currentUserStore,
            IProvider<ErrandPart> errandPartProvider,
            IProvider<ErrandPartStatus> errandPartStatusProvider,
            IDataStore<ErrandPartStatus> errandPartStatusStore,
            INotifier notifier)
        {
            _requestStore = requestStore;
            _viewModel = addOfferViewModel;
            _errandPartProvider = (ErrandPartProvider)errandPartProvider; //Temporary fix
            _errandPartStatusProvider = (ErrandPartStatusProvider)errandPartStatusProvider; //Temporary fix
            _errandPartStatusStore = errandPartStatusStore;
            _notifier = notifier;
            _currentUser = currentUserStore.User!;
        }
        public override void Execute(object? parameter)
        {
            if (_requestStore.Request is null)
            {
                return;
            }

            ErrandPart request = _requestStore.Request;
            if (!_viewModel.IsDataValidate)
            {
                return;
            }

            if (request.Id is null)
            {
                return;
            }

            List<Task> tasks = [];
            tasks.Add(UpdateErrandPart(request));
            tasks.Add(ErrandPartStatusConfirm(request.LastStatus));
            tasks.Add(AddNextStatus(request, parameter));
            Task.WhenAll(tasks);
            ReturnToOffer(); //return or lock?
        }

        private static bool SelfConfirm(object? parameter)
        {
            if (parameter is null)
            {
                return false;
            }

            if (parameter is not string)
            {
                return false;
            }

            if ((string)parameter == "CONFIRM")
            {
                return true;
            }

            return false;
        }
        private async Task AddNextStatus(ErrandPart request, object? parameter)
        {
            ErrandPartStatus newStatus = new(ErrandPartStatus.Status[1])
            {
                CreatedDate = DateTime.Now,
                Reason = "OFFER ADDED",
                ErrandPartId = (int)request.Id!,
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
                    ErrandPartId = (int)request.Id!,
                }));
                await Task.WhenAll(tasks);
            }
        }
        private void AddToStatusStore(ErrandPartStatus status)
        {
            _errandPartStatusStore.Data.Add(status);
        }
        private async Task ErrandPartStatusConfirm(ErrandPartStatus status)
        {
            await _errandPartStatusProvider.ConfirmStatus((int)status.Id!, status.Comment, (int)_currentUser.Id!);
        }
        private async Task ErrandPartStatusNew(ErrandPartStatus status)
        {
            status.Id = await _errandPartStatusProvider.Create(status);
            AddToStatusStore(status);
        }
        private void ReturnToOffer()
        {
            _notifier.ShowSuccess("Dodano ofertę i przekazano do zatwierdzenia!");
            //NavigationService<OffersViewModel> navigationService = _services.GetRequiredService<NavigationService<OffersViewModel>>();
            //navigationService.Navigate();
        }
        private async Task UpdateErrandPart(ErrandPart request)
        {
            double price = _viewModel.PricePerUnit;
            DateTime? deliveryDate = request.ExpectedDeliveryDate;
            request.SetPrice(price);
            await _errandPartProvider.UpdatePrice((int)request.Id!, price);
            await _errandPartProvider.UpdateDeliveryDate((int)request.Id!, deliveryDate);
        }
    }
}