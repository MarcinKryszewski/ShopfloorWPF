using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Shared.BaseClasses
{
    internal abstract class StoreBase<T> : IDataStore<T>
    {
        private readonly IProvider<T> _provider;
        private List<T> _data;
        protected StoreBase(IProvider<T> provider)
        {
            _data = [];
            _provider = provider;
        }
        public List<T> Data
        {
            get
            {
                if (!IsLoaded)
                {
                    Load();
                }

                return _data;
            }
        }
        public bool IsLoaded { get; protected set; }
        public async Task Reload()
        {
            _data = new(await _provider.GetAll());
        }
        protected Task Load()
        {
            _data = new(_provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
    }
}