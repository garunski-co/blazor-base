﻿using System.ComponentModel.DataAnnotations;
using Spent.Commons.Resources;

namespace Spent.Commons.Dtos.Identity;

[DtoResourceType(typeof(AppStrings))]
public class RefreshRequestDto
{
    [Required(ErrorMessage = nameof(AppStrings.RequiredAttribute_ValidationError))]
    public string? RefreshToken { get; set; }
}