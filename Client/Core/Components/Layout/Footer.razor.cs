using System.Linq;
using System.Threading.Tasks;
using Spent.Client.Core.Extensions;
using Spent.Commons.Attributes;
using Spent.Commons.Infra;

namespace Spent.Client.Core.Components.Layout;

public partial class Footer
{
    [AutoInject] private BitThemeManager bitThemeManager = default!;
    [AutoInject] private IBitDeviceCoordinator BitDeviceCoordinator { get; set; } = default!;

    private BitDropdownItem<string>[] cultures = default!;

    protected override Task OnInitAsync()
    {
        cultures = CultureInfoManager.SupportedCultures
            .Select(sc => new BitDropdownItem<string> { Value = sc.code, Text = sc.name })
            .ToArray();

        selectedCulture = CultureInfoManager.GetCurrentCulture();

        return base.OnInitAsync();
    }

    private string? selectedCulture;

    private async Task OnCultureChanged()
    {
        await JsRuntime.SetCookie(".AspNetCore.Culture", $"c={selectedCulture}|uic={selectedCulture}",
            expiresIn: 30 * 24 * 3600, rememberMe: true);

        await StorageService.SetItem("Culture", selectedCulture, persistent: true);

        // Relevant in the context of Blazor Hybrid, where the reloading of the web view doesn't result in the resetting of all static in memory data on the client side
        CultureInfoManager.SetCurrentCulture(selectedCulture);

        NavigationManager.Refresh(forceReload: true);
    }

    private async Task ToggleTheme()
    {
        await BitDeviceCoordinator.ApplyTheme(await bitThemeManager.ToggleDarkLightAsync() == "dark");
    }
}
