using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HPhrases
{
    internal class HPhrase : IModel
    {
        required public int Id { get; init; }
        public string Name { get; } = string.Empty;
        required public string Code { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            throw new System.NotImplementedException();
        }
    }
}