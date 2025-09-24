using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.RiskEvaluations
{
    internal class RiskEvaluationCreation : ModelValidationBase, IModelCreationModel<RiskEvaluation>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RiskEvaluation CreateModel(int id)
        {
            return new RiskEvaluation()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}