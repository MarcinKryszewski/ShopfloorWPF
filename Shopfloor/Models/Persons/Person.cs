using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Persons
{
    internal class Person : IModel
    {
        public string FullName => $"{Name} {Surname}";
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public Workshop? Workshop { get; set; }
        public int WorkshopId { get; init; }
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            if (data is not PersonCreation)
            {
                return;
            }

            PersonCreation creation = (PersonCreation)data;

            Name = creation.Name;
            Surname = creation.Surname;
            Workshop = creation.Workshop;
            Username = creation.Username;
        }
    }
}