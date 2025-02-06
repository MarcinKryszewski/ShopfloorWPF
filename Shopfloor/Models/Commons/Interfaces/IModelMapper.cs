using System;

namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IModelMapper<T, TCreate>
        where T : IModel
        where TCreate : IModelCreationModel<T>
    {
        public static abstract T ToModel(int id, TCreate creationModel);
        public static abstract TCreate ToCreationModel(T model);
    }
}