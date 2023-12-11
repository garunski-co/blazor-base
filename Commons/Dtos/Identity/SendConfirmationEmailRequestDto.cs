﻿
using System.ComponentModel.DataAnnotations;
using Spent.Commons.Resources;

namespace Spent.Commons.Dtos.Identity;

[DtoResourceType(typeof(AppStrings))]
public class SendConfirmationEmailRequestDto
{
    [Required(ErrorMessage = nameof(AppStrings.RequiredAttribute_ValidationError))]
    [EmailAddress(ErrorMessage = nameof(AppStrings.EmailAddressAttribute_ValidationError))]
    [Display(Name = nameof(AppStrings.Email))]
    public string? Email { get; set; }
}