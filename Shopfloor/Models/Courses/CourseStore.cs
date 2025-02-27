using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Courses
{
    internal class CourseStore : IStore<Course>
    {
        private readonly List<Course> _data = [];
        public List<Course> Data => _data;
        public Task AddItem(Course item)
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