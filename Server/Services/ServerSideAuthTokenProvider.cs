﻿using System.Reflection;
using Spent.Client.Core.Services;
using Microsoft.JSInterop;

namespace Spent.Server.Services;

/// <summary>
/// The <see cref="ClientSideAuthTokenProvider"/> reads the token from the local storage,
/// but during prerendering, there is no access to localStorage or the stored cookies.
/// However, the cookies are sent automatically in http request and The <see cref="ServerSideAuthTokenProvider"/> provides that token to the application.
/// </summary>
public partial class ServerSideAuthTokenProvider : IAuthTokenProvider
{
    [AutoInject] private readonly IHttpContextAccessor httpContextAccessor = default!;
    [AutoInject] private readonly IJSRuntime jsRuntime = default!;
    [AutoInject] private readonly IStorageService storageService = default!;

    private static readonly PropertyInfo IsInitializedProp = Assembly.Load("Microsoft.AspNetCore.Components.Server")
                                                                .GetType("Microsoft.AspNetCore.Components.Server.Circuits.RemoteJSRuntime")!
                                                                .GetProperty("IsInitialized")!;

    public bool IsInitialized => jsRuntime.GetType().Name is not "UnsupportedJavaScriptRuntime" && (bool)IsInitializedProp.GetValue(jsRuntime)!;

    public async Task<string?> GetAccessTokenAsync()
    {
        if (IsInitialized)
        {
            return await storageService.GetItem("access_token");
        }

        return httpContextAccessor.HttpContext?.Request.Cookies["access_token"];
    }
}
