using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Manufacturers
{
    internal class ManufacturerModel : IModel
    {
        public int Id
        {
            get;
            init;
        }
        public string Name { get; set; } = string.Empty;
    }
}