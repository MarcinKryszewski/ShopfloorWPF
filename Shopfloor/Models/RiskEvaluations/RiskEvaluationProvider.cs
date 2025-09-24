using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
namespace Shopfloor.Models.RiskEvaluations
{
    internal class RiskEvaluationProvider : IProvider<RiskEvaluation, RiskEvaluationCreation>
    {
        public Task<int> Create(RiskEvaluationCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<RiskEvaluation>> GetAll()
        {
            IEnumerable<RiskEvaluation> data = [
                new RiskEvaluation { Id = 1, Name = "TEST1", },
                new RiskEvaluation { Id = 2, Name = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<RiskEvaluation?> GetById(int id)
        {
            List<RiskEvaluation> data = [
                new RiskEvaluation { Id = 1, Name = "TEST1", },
                new RiskEvaluation { Id = 2, Name = "TEST2", },
            ];

            RiskEvaluation? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(RiskEvaluation item)
        {
            return Task.CompletedTask;
        }
    }
}