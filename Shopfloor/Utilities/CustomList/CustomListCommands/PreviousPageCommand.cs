using Shopfloor.Shared.Commands;

namespace Shopfloor.Utilities.CustomList.CustomListCommands
{
    internal sealed class PreviousPageCommand : CommandBase
    {
        private readonly SearchableModelList _displayList;
        public PreviousPageCommand(SearchableModelList displayList)
        {
            _displayList = displayList;
        }
        public override void Execute(object? parameter)
        {
            _displayList.PagePrev();
        }
    }
}