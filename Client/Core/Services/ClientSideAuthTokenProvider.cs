namespace Spent.Client.Core.Services;

public partial class ClientSideAuthTokenProvider : IAuthTokenProvider
{
    [AutoInject] private readonly IStorageService storageService = default!;

    public bool IsInitialized => true;

    public async Task<string?> GetAccessTokenAsync()
    {
        return await storageService.GetItem("access_token");
    }
}
