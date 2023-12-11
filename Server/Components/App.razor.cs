using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace Spent.Server.Components;

[StreamRendering(enabled: true)]
public partial class App
{
    [CascadingParameter] HttpContext HttpContext { get; set; } = default!;
}
