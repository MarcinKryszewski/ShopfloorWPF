using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Persons
{
    internal class PersonCreation : ModelValidationBase, IModelCreationModel<Person>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public Workshop? Workshop { get; set; }
        public int WorkshopId { get; set; }
        public Person CreateModel(int id)
        {
            return new Person
            {
                Id = id,
                Name = Name,
                Surname = Surname,
                Workshop = Workshop,
                WorkshopId = WorkshopId,
            };
        }
    }
}