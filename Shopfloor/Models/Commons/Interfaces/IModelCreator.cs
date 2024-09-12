using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Interfaces
{
    public interface IModelCreator<T, TDto>
        where T : IModel
        where TDto : IModelDto
    {
        public T Create(TDto dto);
    }
}