using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Interfaces
{
    internal interface IModelCreator<T, TDto>
        where T : IModel
        where TDto : IModelDto
    {
        public T Create(TDto dto);
    }
}