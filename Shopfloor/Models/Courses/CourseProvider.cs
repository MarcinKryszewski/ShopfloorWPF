using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Courses
{
    internal class CourseProvider : IProvider<Course, CourseCreation>
    {
        public Task<int> Create(CourseCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Course>> GetAll()
        {
            IEnumerable<Course> data = [
                new Course { Id = 1, Name = "Course1", },
                new Course { Id = 2, Name = "Course2", },
                new Course { Id = 3, Name = "Course3", },
                new Course { Id = 4, Name = "Course4", },
            ];
            return Task.FromResult(data);
        }
        public Task<Course?> GetById(int id)
        {
            List<Course> data = [
                new Course { Id = 1, Name = "Course1", },
                new Course { Id = 2, Name = "Course2", },
                new Course { Id = 3, Name = "Course3", },
                new Course { Id = 4, Name = "Course4", },
            ];

            Course? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Course item)
        {
            return Task.CompletedTask;
        }
    }
}