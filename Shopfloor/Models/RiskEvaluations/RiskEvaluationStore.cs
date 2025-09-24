using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.RiskEvaluations
{
    internal class RiskEvaluationStore : IStore<RiskEvaluation>
    {
        private readonly List<RiskEvaluation> _data = [];
        public List<RiskEvaluation> Data => _data;
        public Task AddItem(RiskEvaluation item)
        {
            _data.Add(item);
            return Task.CompletedTask;
        }
        public async Task ReloadData()
        {
            await Task.Delay(0);
        }
    }
}