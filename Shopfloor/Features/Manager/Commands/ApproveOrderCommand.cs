using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Stores;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Shopfloor.Features.Manager.Commands
{
    internal sealed class ApproveOrderCommand : CommandBase
    {
        private readonly SelectedRequestStore _requestStore;
        private readonly OrderApproveViewModel _viewModel;
        private readonly IServiceProvider _services;
        private readonly IServiceProvider _databaseServices;
        private readonly ErrandPartStatusProvider _provider;
        private readonly User _currentUser;
        public ApproveOrderCommand(SelectedRequestStore requestStore, OrderApproveViewModel viewModel, IServiceProvider databaseServices, IServiceProvider userServices, IServiceProvider mainServices)
        {
            _requestStore = requestStore;
            _viewModel = viewModel;
            _services = mainServices;
            _currentUser = userServices.GetRequiredService<CurrentUserStore>().User!;
            _databaseServices = databaseServices;
            _provider = _databaseServices.GetRequiredService<ErrandPartStatusProvider>();
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
            _services.GetRequiredService<Notifier>().ShowSuccess("Dodano ofertę i przekazano do zatwierdzenia!");
            NavigationService<OrderApproveViewModel> navigationService = _services.GetRequiredService<NavigationService<OrderApproveViewModel>>();
            navigationService.Navigate();
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
            int nextId = await _provider.Create(newStatus);

            if ((nextStatusName == ErrandPartStatus.Status[8]) | (nextStatusName == ErrandPartStatus.Status[9]))
            {
                await _provider.ConfirmStatus(nextId, _viewModel.Comment, (int)_currentUser.Id!);
            }
        }
        private void AddToStore(ErrandPartStatus status)
        {
            ErrandPartStatusStore store = _databaseServices.GetRequiredService<ErrandPartStatusStore>();
            store.Data.Add(status);
        }
    }
}