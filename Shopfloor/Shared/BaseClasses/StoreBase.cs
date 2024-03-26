using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

internal abstract class StoreBase<T> : IDataStore<T>
{
    protected List<T> _data;
    protected readonly IProvider<T> _provider;
    public StoreBase(IProvider<T> provider)
    {
        _data = [];
        _provider = provider;
    }
    public List<T> GetData()
    {
        if (!IsLoaded) Load();
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