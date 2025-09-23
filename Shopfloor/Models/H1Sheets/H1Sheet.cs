using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.H1Sheets
{
    internal class H1Sheet : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            throw new NotImplementedException();
        }
    }
}