using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Parts
{
    internal class PartValidation : IModelValidation<PartCreationModel>
    {
        public void Validate(PartCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}