﻿using System;
using System.Threading.Tasks;
using Spent.Commons.Dtos;
using Spent.Commons.Dtos.Identity;
using Spent.Commons.Exceptions;
using Spent.Commons.Extensions;
using Spent.Commons.Resources;

namespace Spent.Client.Core.Components.Pages.Identity;

public partial class SignUpPage
{
    private bool isLoading;
    private bool isSignedUp;
    private string? signUpMessage;
    private BitMessageBarType signUpMessageType;
    private SignUpRequestDto signUpModel = new();


    protected override async Task OnAfterFirstRenderAsync()
    {
        await base.OnAfterFirstRenderAsync();

        if ((await AuthenticationStateTask).User.IsAuthenticated())
        {
            NavigationManager.NavigateTo("/");
        }
    }

    private async Task DoSignUp()
    {
        if (isLoading) return;

        isLoading = true;
        signUpMessage = null;

        try
        {
            await HttpClient.PostAsJsonAsync("Identity/SignUp", signUpModel, AppJsonContext.Default.SignUpRequestDto, CurrentCancellationToken);

            isSignedUp = true;
        }
        catch (ResourceValidationException e)
        {
            signUpMessageType = BitMessageBarType.Error;
            signUpMessage = string.Join(Environment.NewLine, e.Payload.Details.SelectMany(d => d.Errors).Select(e => e.Message));
        }
        catch (KnownException e)
        {
            signUpMessage = e.Message;
            signUpMessageType = BitMessageBarType.Error;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task DoResendLink()
    {
        if (isLoading) return;

        isLoading = true;
        signUpMessage = null;

        try
        {
            await HttpClient.PostAsJsonAsync("Identity/SendConfirmationEmail", new() { Email = signUpModel.Email }, AppJsonContext.Default.SendConfirmationEmailRequestDto, CurrentCancellationToken);

            signUpMessageType = BitMessageBarType.Success;
            signUpMessage = Localizer[nameof(AppStrings.ResendConfirmationLinkMessage)];
        }
        catch (KnownException e)
        {
            signUpMessage = e.Message;
            signUpMessageType = BitMessageBarType.Error;
        }
        finally
        {
            isLoading = false;
        }
    }
}
