using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

internal abstract class StoreBase<T>
{
    protected List<T> _data;
    protected readonly IProvider<T> _provider;
    protected readonly ICombiner _combiner;
    public StoreBase(IProvider<T> provider, ICombiner combiner)
    {
        _data = [];
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
    protected Task Load()
    {
        _data = new(_provider.GetAll().Result);
        IsLoaded = true;
        return Task.CompletedTask;
    }
    public async Task Reload()
    {
        _data = new(await _provider.GetAll());
    }
}