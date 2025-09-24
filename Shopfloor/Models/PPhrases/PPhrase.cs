using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.PPhrases
{
    internal class PPhrase : IModel
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