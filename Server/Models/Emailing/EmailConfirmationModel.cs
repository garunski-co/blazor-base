namespace Spent.Server.Models.Emailing;

public class EmailConfirmationModel
{
    public EmailConfirmationModel() { }

    public EmailConfirmationModel(string? confirmationLink)
    {
        ConfirmationLink = confirmationLink;
    }

    public string? ConfirmationLink { get; set; }
}
