using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class Line : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            if (data is not LineCreation)
            {
                return;
            }

            LineCreation creation = (LineCreation)data;

            Name = creation.Name;
        }
    }
}