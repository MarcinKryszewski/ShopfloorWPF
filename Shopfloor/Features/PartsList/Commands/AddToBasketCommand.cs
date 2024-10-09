using System.Linq;
using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Models.Parts;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.PartsList.Commands
{
    internal class AddToBasketCommand : CommandBase
    {
        private readonly PartsBasketContext _basket;
        private readonly INotifier _notifier;
        public AddToBasketCommand(PartsBasketContext basket, INotifier notifier)
        {
            _basket = basket;
            _notifier = notifier;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is not PartModel)
            {
                string objectIsNotPart = "Obiekt nie jest częścią";
                _notifier.ShowError(objectIsNotPart);
                return;
            }

            PartModel part = (PartModel)parameter;

            if (_basket.Parts.Any(x => x.Part == part))
            {
                string partInTheBasket = "Część znajduje się już na liście!";
                _notifier.ShowInformation(partInTheBasket);
                return;
            }
            string partAddToBasket = $"Dodano {part.NameOriginal} do listy";
            _basket.Parts.Add(new WorkOrderPartCreationModel() { Part = part, PartId = part.Id });
            _notifier.ShowSuccess(partAddToBasket);
        }
    }
}