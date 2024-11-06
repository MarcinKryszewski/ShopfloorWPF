using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivityTypes
{
    internal class ActivityTypeCreation : ModelValidationBase, IModelCreationModel<ActivityType>
    {
        required public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ActivityType CreateModel(int id)
        {
            return new ActivityType()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}