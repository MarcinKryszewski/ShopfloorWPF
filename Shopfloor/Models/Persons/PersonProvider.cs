using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Persons
{
    internal class PersonProvider : IProvider<Person, PersonCreation>
    {
        public Task<int> Create(PersonCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Person>> GetAll()
        {
            IEnumerable<Person> data = [
                new Person { Id = 1, Name = "Alice", Surname = "Johnson", WorkshopId = 1 },
                new Person { Id = 2, Name = "Bob", Surname = "Smith", WorkshopId = 2 },
                new Person { Id = 3, Name = "Carol", Surname = "Miller", WorkshopId = 3 },
                new Person { Id = 4, Name = "David", Surname = "Brown", WorkshopId = 1 },
                new Person { Id = 5, Name = "Eve", Surname = "Davis", WorkshopId = 2 },
                new Person { Id = 6, Name = "Frank", Surname = "Wilson", WorkshopId = 3 },
                new Person { Id = 7, Name = "Grace", Surname = "Taylor", WorkshopId = 1 },
                new Person { Id = 8, Name = "Hank", Surname = "Anderson", WorkshopId = 2 },
                new Person { Id = 9, Name = "Ivy", Surname = "Thomas", WorkshopId = 3 },
                new Person { Id = 10, Name = "Jack", Surname = "White", WorkshopId = 1 }
            ];

            return Task.FromResult(data);
        }
        public Task<Person?> GetById(int id)
        {
            List<Person> data = [
                new Person { Id = 1, Name = "Alice", Surname = "Johnson", WorkshopId = 1 },
                new Person { Id = 2, Name = "Bob", Surname = "Smith", WorkshopId = 2 },
                new Person { Id = 3, Name = "Carol", Surname = "Miller", WorkshopId = 3 },
                new Person { Id = 4, Name = "David", Surname = "Brown", WorkshopId = 1 },
                new Person { Id = 5, Name = "Eve", Surname = "Davis", WorkshopId = 2 },
                new Person { Id = 6, Name = "Frank", Surname = "Wilson", WorkshopId = 3 },
                new Person { Id = 7, Name = "Grace", Surname = "Taylor", WorkshopId = 1 },
                new Person { Id = 8, Name = "Hank", Surname = "Anderson", WorkshopId = 2 },
                new Person { Id = 9, Name = "Ivy", Surname = "Thomas", WorkshopId = 3 },
                new Person { Id = 10, Name = "Jack", Surname = "White", WorkshopId = 1 }
            ];

            Person? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Person item)
        {
            return Task.CompletedTask;
        }
    }
}