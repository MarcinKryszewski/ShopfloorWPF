using Shopfloor.Shared.Commands;

namespace Shopfloor.Utilities.CustomList.CustomListCommands
{
    internal sealed class NextPageCommand : CommandBase
    {
        private readonly SearchableModelList _displayList;
        public NextPageCommand(SearchableModelList displayList)
        {
            _displayList = displayList;
        }
        public override void Execute(object? parameter)
        {
            _displayList.PageNext();
        }
    }
}