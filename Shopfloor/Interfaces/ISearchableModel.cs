namespace Shopfloor.Interfaces
{
    internal interface ISearchableModel
    {
        int? Id { get; }
        string SearchValue { get; }
    }
}