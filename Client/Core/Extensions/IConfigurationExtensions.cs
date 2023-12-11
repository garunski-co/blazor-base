using System;
using Microsoft.Extensions.Configuration;

namespace Spent.Client.Core.Extensions;
public static class IConfigurationExtensions
{
    public static string GetApiServerAddress(this IConfiguration configuration)
    {
        var apiServerAddress = configuration.GetValue("ApiServerAddress", defaultValue: "api/")!;

        return Uri.TryCreate(apiServerAddress, UriKind.RelativeOrAbsolute, out _) ? apiServerAddress : throw new InvalidOperationException($"Api server address {apiServerAddress} is invalid");
    }
}
