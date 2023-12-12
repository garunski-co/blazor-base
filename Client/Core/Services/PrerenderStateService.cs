namespace Spent.Client.Core.Services;

/// <summary>
/// For more information <see cref="IPrerenderStateService"/> docs.
/// </summary>
public class PrerenderStateService : IPrerenderStateService, IAsyncDisposable
{
    private readonly PersistentComponentState? _persistentComponentState;

    private readonly PersistingComponentStateSubscription? _subscription;

    private readonly ConcurrentDictionary<string, object?> _values = new();

    public PrerenderStateService(PersistentComponentState? persistentComponentState = null)
    {
        _subscription = persistentComponentState?.RegisterOnPersisting(PersistAsJson, AppRenderMode.Current);
        _persistentComponentState = persistentComponentState;
    }

    public async ValueTask DisposeAsync()
    {
        if (AppRenderMode.PrerenderEnabled is false)
        {
            return;
        }

        _subscription?.Dispose();
    }

    public async Task<T> GetValue<T>(string key, Func<Task<T?>> factory)
    {
        if (AppRenderMode.PrerenderEnabled is false)
        {
            return await factory();
        }

        if (_persistentComponentState!.TryTakeFromJson(key, out T? value))
        {
            return value;
        }

        var result = await factory();
        Persist(key, result);
        return result;
    }

    private void Persist<T>(string key, T value)
    {
        if (AppRenderMode.PrerenderEnabled is false)
        {
            return;
        }

        _values.TryRemove(key, out var _);
        _values.TryAdd(key, value);
    }

    private async Task PersistAsJson()
    {
        foreach (var item in _values)
        {
            _persistentComponentState!.PersistAsJson(item.Key, item.Value);
        }
    }
}
