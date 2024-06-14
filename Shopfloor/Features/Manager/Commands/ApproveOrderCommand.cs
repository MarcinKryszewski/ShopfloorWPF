using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Features.Manager.Commands
{
    internal sealed class ApproveOrderCommand : CommandBase
    {
        private const string _orderApproved = "Dodano ofertę i przekazano do zatwierdzenia!";

        private readonly INavigationService _navigationService;
        private readonly ErrandPartStatusStore _errandPartStatusStore;
        private readonly INotifier _notifier;
        private readonly SelectedRequestStore _requestStore;
        private readonly OrderApproveViewModel _viewModel;
        private readonly ErrandPartStatusProvider _provider;
        private readonly User _currentUser;
        public ApproveOrderCommand(INavigationService navigationService, ErrandPartStatusStore errandPartStatusStore, INotifier notifier, SelectedRequestStore requestStore, OrderApproveViewModel viewModel, ICurrentUserStore currentUserStore, ErrandPartStatusProvider errandPartStatusProvider)
        {
            _navigationService = navigationService;
            _errandPartStatusStore = errandPartStatusStore;
            _notifier = notifier;
            _requestStore = requestStore;
            _viewModel = viewModel;
            _currentUser = currentUserStore.User!;
            _provider = errandPartStatusProvider;
        }
        public override void Execute(object? parameter)
        {
            if (_requestStore.Request is null) return;

            ErrandPart request = _requestStore.Request;
            if (!_viewModel.IsDataValidate) return;
            if (request.Id is null) return;

            List<Task> tasks = [];
            tasks.Add(ErrandPartUpdateStatus(request.LastStatus));
            tasks.Add(ErrandPartNewStatus(request, parameter));
            Task.WhenAll(tasks);
            ReturnToApprovals();
        }
        private void ReturnToApprovals()
        {
            _notifier.ShowSuccess(_orderApproved);
            _navigationService.NavigateTo<OrderApproveViewModel>();
        }
        private async Task ErrandPartUpdateStatus(ErrandPartStatus requestStatus)
        {
            requestStatus.Comment = _viewModel.Comment;
            requestStatus.CompletedById = (int)_currentUser.Id!;

            await _provider.ConfirmStatus((int)requestStatus.Id!, _viewModel.Comment, (int)_currentUser.Id!);
        }
        private async Task ErrandPartNewStatus(ErrandPart request, object? parameter)
        {
            if (parameter is null) return;
            if (parameter is not string) return;
            string nextStatusName = parameter switch
            {
                "CONFIRM" => ErrandPartStatus.Status[3],
                "HOLD" => ErrandPartStatus.Status[8],
                "CANCEL" => ErrandPartStatus.Status[9],
                _ => ErrandPartStatus.Status[-1],
            };
            ErrandPartStatus newStatus = new(nextStatusName)
            {
                ErrandPartId = (int)request.Id!,
                CreatedDate = DateTime.Now,
                Reason = "APPROVAL STATUS CHANGED"
            };
            AddToStore(newStatus);
            newStatus.Id = await _provider.Create(newStatus);

            if ((nextStatusName == ErrandPartStatus.Status[8]) | (nextStatusName == ErrandPartStatus.Status[9]))
            {
                await _provider.ConfirmStatus((int)newStatus.Id!, _viewModel.Comment, (int)_currentUser.Id!);
            }
        }
        private void AddToStore(ErrandPartStatus status)
        {
            List<ErrandPartStatus> errandPartStatuses = _errandPartStatusStore.Data;
            errandPartStatuses.Add(status);
        }
    }
}