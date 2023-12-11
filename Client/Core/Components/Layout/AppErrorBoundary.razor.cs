﻿
using System;
using System.Threading.Tasks;
using Spent.Commons.Attributes;
using Spent.Commons.Infra;

namespace Spent.Client.Core.Components.Layout;

/// <summary>
/// https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/handle-errors
/// </summary>
public partial class AppErrorBoundary
{
    private bool showException;

    [AutoInject] private IExceptionHandler exceptionHandler = default!;

    [AutoInject] private NavigationManager navigationManager = default!;

    protected override void OnInitialized()
    {
        showException = BuildConfiguration.IsDebug();
    }

    protected override async Task OnErrorAsync(Exception exception)
    {
        exceptionHandler.Handle(exception);
    }

    private void Refresh()
    {
        navigationManager.Refresh(forceReload: true);
    }

    private void GoHome()
    {
        navigationManager.NavigateTo("/", true);
    }
}
