namespace Spent.Server;

public class IdentitySettings
{
    public TimeSpan BearerTokenExpiration { get; set; }

    public TimeSpan RefreshTokenExpiration { get; set; }

    public string Issuer { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public string IdentityCertificatePassword { get; set; } = default!;

    public bool PasswordRequireDigit { get; set; }

    public int PasswordRequiredLength { get; set; }

    public bool PasswordRequireNonAlphanumeric { get; set; }

    public bool PasswordRequireUppercase { get; set; }

    public bool PasswordRequireLowercase { get; set; }

    public bool RequireUniqueEmail { get; set; }

    public TimeSpan ConfirmationEmailResendDelay { get; set; }

    public TimeSpan ResetPasswordEmailResendDelay { get; set; }
}
