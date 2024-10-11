using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.WorkOrderAddNew.Commands
{
    internal class RemovePartFromBasketCommand : CommandBase
    {
        private readonly PartsBasketContext _partsBasket;
        public RemovePartFromBasketCommand(PartsBasketContext partsBasket)
        {
            _partsBasket = partsBasket;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is not WorkOrderPartCreationModel)
            {
                return;
            }

            _partsBasket.Parts.Remove((WorkOrderPartCreationModel)parameter);
        }
    }
}