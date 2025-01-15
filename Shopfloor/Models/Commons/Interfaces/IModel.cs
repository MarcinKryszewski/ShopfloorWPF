namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IModel
    {
        public int Id { get; init; }
        public string Name { get; }
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel;
    }
}