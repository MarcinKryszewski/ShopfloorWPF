using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HPhrases
{
    internal class HPhraseCreation : ModelValidationBase, IModelCreationModel<HPhrase>
    {
        public int Id { get; set; }
        public string Name { get; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public HPhrase CreateModel(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}