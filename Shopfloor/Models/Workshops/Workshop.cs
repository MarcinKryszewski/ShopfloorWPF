using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Workshops
{
    internal class Workshop : IModel
    {
        required public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            if (data is not WorkshopCreation)
            {
                return;
            }

            WorkshopCreation creation = (WorkshopCreation)data;
        }
    }
}