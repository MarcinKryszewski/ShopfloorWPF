using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

internal abstract class StoreBase<T> : IDataStore<T>
{
    protected List<T> _data = new List<T>();
    protected readonly IProvider<T> _provider;
    protected readonly ICombiner _combiner;

    public StoreBase(IProvider<T> provider, ICombiner combiner)
    {
        _provider = provider;
        _combiner = combiner;
    }

    public List<T> GetData(bool shouldCombine = false)
    {
        if (!IsLoaded) Load();
        if (shouldCombine) _combiner.Combine().Wait();
        return _data;
    }

    public bool IsLoaded { get; protected set; }

    protected virtual async Task Load()
    {
        _data = new List<T>(await _provider.GetAll());
        IsLoaded = true;
    }

    public async Task Reload()
    {
        _data = new(await _provider.GetAll());
    }
}