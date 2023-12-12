namespace Spent.Server;

public class AppSettings
{
    public IdentitySettings IdentitySettings { get; set; } = default!;

    public EmailSettings EmailSettings { get; set; } = default!;

    public HealthCheckSettings HealthCheckSettings { get; set; } = default!;

    public string UserProfileImagesDir { get; set; } = default!;

    public string WebServerAddress { get; set; } = default!;
}
