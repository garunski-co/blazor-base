﻿namespace Spent.Client.Core.Services;

/// <summary>
///     For more information <see cref="IPubSubService" /> docs.
/// </summary>
[UsedImplicitly]
public partial class PubSubService : IPubSubService
{
    private readonly ConcurrentDictionary<string, List<Func<object?, Task>>> _handlers = new();

    [AutoInject]
    private readonly IServiceProvider _serviceProvider = default!;

    public void Publish(string message, object? payload)
    {
        if (!_handlers.TryGetValue(message, out var messageHandlers))
        {
            return;
        }

        foreach (var handler in messageHandlers)
        {
            handler(payload)
                .ContinueWith(t => _serviceProvider.GetRequiredService<IExceptionHandler>().Handle(t.Exception!),
                    TaskContinuationOptions.OnlyOnFaulted);
        }
    }

    public Action Subscribe(string message, Func<object?, Task> handler)
    {
        var messageHandlers = _handlers.TryGetValue(message, out var value) ? value : _handlers[message] = [];

        messageHandlers.Add(handler);

        return () => messageHandlers.Remove(handler);
    }
}
