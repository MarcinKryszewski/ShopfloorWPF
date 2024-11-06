using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivityTypes
{
    internal class ActivityType : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            if (data is not ActivityTypeCreation)
            {
                return;
            }

            ActivityTypeCreation creation = (ActivityTypeCreation)data;

            Name = creation.Name;
        }
    }
}