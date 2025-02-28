using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Attendences;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Roots
{
    internal class TrainingsRoot : IRoot
    {
        public event EventHandler? DataChanged;
    }
}