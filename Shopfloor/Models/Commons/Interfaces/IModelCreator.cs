namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IModelCreator<T, TDto>
        where T : IModel
        where TDto : IModelDto
    {
        public T Create(TDto dto);
    }
}