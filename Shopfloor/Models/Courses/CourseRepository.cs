using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Courses
{
    internal class CourseRepository : IRepository<Course, CourseCreation>
    {
        private readonly IProvider<Course, CourseCreation> _provider;
        private readonly IStore<Course> _store;
        private bool _dataLoaded = false;
        public CourseRepository(IStore<Course> store, IProvider<Course, CourseCreation> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<Course> Create(CourseCreation item)
        {
            int id = await _provider.Create(item);
            Course model = item.CreateModel(id);
            _store.Data.Add(model);
            return model;
        }
        public async Task Delete(int id)
        {
            Course? item = _store.Data.Find(x => x.Id == id);
            if (item == null)
            {
                string errorText = "ERROR";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            try
            {
                await _provider.Delete(id);
                _store.Data.Remove(item);
            }
            catch (Exception)
            {
                string errorText = "ERROR";
                await Task.FromException(new InvalidOperationException(errorText));
            }
        }
        public async Task<List<Course>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<Course> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(CourseCreation item)
        {
            Course? existingData = _store.Data.Find(x => x.Id == item.Id);

            if (existingData is null)
            {
                string errorText = "ERROR";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            await _provider.Update(existingData);
            existingData.SetValues(item);

            await Task.CompletedTask;
        }
    }
}