using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
namespace Shopfloor.Models.PPhrases
{
    internal class PPhraseCreation : ModelValidationBase, IModelCreationModel<PPhrase>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PPhrase CreateModel(int id)
        {
            return new PPhrase()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}