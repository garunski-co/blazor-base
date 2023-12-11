using System.Reflection;

namespace Microsoft.Extensions.Configuration;

public static class IConfigurationBuilderExtensions
{
    public static void AddClientConfigurations(this IConfigurationBuilder builder)
    {
        var assembly = Assembly.Load("Spent.Client.Core");
        builder.AddJsonStream(assembly.GetManifestResourceStream("Spent.Client.Core.appsettings.json")!);
    }
}
