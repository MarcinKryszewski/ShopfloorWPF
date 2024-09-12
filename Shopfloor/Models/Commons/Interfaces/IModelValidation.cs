using Shopfloor.Models.Commons.BaseClasses;

namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IModelValidation<T>
        where T : ModelValidationBase
    {
        public void Validate(T item);
    }
}