namespace Spent.Server;

[UsedImplicitly]
public class EmailSettings
{
    public string Host { get; set; } = default!;

    /// <summary>
    ///     If true, the web app tries to store emails as .eml file in the bin/Debug/net8.0/sent-emails folder instead of
    ///     sending them using smtp server (recommended for testing purposes only).
    /// </summary>
    public bool UseLocalFolderForEmails => Host is "LocalFolder";

    public int Port { get; set; }

    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string DefaultFromEmail { get; set; } = default!;

    public string DefaultFromName { get; set; } = default!;

    public bool HasCredential => string.IsNullOrEmpty(UserName) is false && string.IsNullOrEmpty(Password) is false;
}
