﻿using Spent.Commons.Dtos.Identity;
using Spent.Commons.Extensions;

namespace Spent.Client.Core.Components.Pages.Identity;

public partial class ResetPasswordPage
{
    private readonly ResetPasswordRequestDto _resetPasswordModel = new();

    private bool _isLoading;

    private string? _resetPasswordMessage;

    private BitMessageBarType _resetPasswordMessageType;

    [Parameter]
    [SupplyParameterFromQuery]
    public string? Email { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string? Token { get; set; }

    protected override Task OnInitAsync()
    {
        _resetPasswordModel.Email = Email;
        _resetPasswordModel.Token = Token;

        return base.OnInitAsync();
    }

    protected override async Task OnAfterFirstRenderAsync()
    {
        await base.OnAfterFirstRenderAsync();

        if ((await AuthenticationStateTask).User.IsAuthenticated())
        {
            NavigationManager.NavigateTo("/");
        }
    }

    private async Task DoSubmit()
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;
        _resetPasswordMessage = null;

        try
        {
            await HttpClient.PostAsJsonAsync("Identity/ResetPassword", _resetPasswordModel,
                AppJsonContext.Default.ResetPasswordRequestDto, CurrentCancellationToken);

            _resetPasswordMessageType = BitMessageBarType.Success;

            _resetPasswordMessage = Localizer[nameof(AppStrings.PasswordChangedSuccessfullyMessage)];
        }
        catch (KnownException e)
        {
            _resetPasswordMessageType = BitMessageBarType.Error;

            _resetPasswordMessage = e.Message;
        }
        finally
        {
            _isLoading = false;
        }
    }
}
