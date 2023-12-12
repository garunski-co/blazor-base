namespace Spent.Client.Core.Services;

public partial class MessageBoxService
{
    [AutoInject] private readonly IPubSubService pubSubService = default!;

    public async Task Show(string message, string title = "")
    {
        TaskCompletionSource<object?> tcs = new();
        pubSubService.Publish(PubSubMessages.ShowMessage, (message, title, tcs));
        await tcs.Task;
    }
}
