﻿
using System.Reflection;
using Spent.Client.Core.Services.HttpMessageHandlers;
using Spent.Client.Maui.Services;

namespace Spent.Client.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var assembly = typeof(MainLayout).GetTypeInfo().Assembly;

        builder
            .UseMauiApp<App>()
            .Configuration.AddClientConfigurations();

        var services = builder.Services;

        services.AddMauiBlazorWebView();

        if (BuildConfiguration.IsDebug())
        {
            services.AddBlazorWebViewDeveloperTools();
        }

        Uri.TryCreate(builder.Configuration.GetApiServerAddress(), UriKind.Absolute, out var apiServerAddress);

        services.AddTransient(sp =>
        {
            var handler = sp.GetRequiredService<RequestHeadersDelegationHandler>();
            HttpClient httpClient = new(handler)
            {
                BaseAddress = apiServerAddress
            };
            return httpClient;
        });

        services.AddTransient<IStorageService, MauiStorageService>();

        services.AddClientMauiServices();

        var mauiApp = builder.Build();

        return mauiApp;
    }
}