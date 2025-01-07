using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Roots
{
    internal class ActivitiesDataRoot : IRoot
    {
        private readonly DataRoot _data;
        public ActivitiesDataRoot(DataRoot data)
        {
            _data = data;
        }
        public event EventHandler? DataChanged;
        public async Task<List<Line>> GetLines()
        {
            return (await _data.GetLine()).ToList();
        }
        public async Task<List<Machine>> GetMachines()
        {
            return (await _data.GetMachine()).ToList();
        }
        public Task<List<Occurance>> GetOccurencies()
        {
            List<Occurance> occurances = [];
            foreach (OccuranceUnit item in Enum.GetValues(typeof(OccuranceUnit)))
            {
                occurances.Add(new()
                {
                    Unit = item,
                });
            }
            return Task.FromResult(occurances);
        }
        public async Task<List<ActivityType>> GetTypes()
        {
            return (await _data.GetActivityType()).ToList();
        }
        public async Task<List<Workshop>> GetWorkshops()
        {
            return (await _data.GetWorkshop()).ToList();
        }
    }
}