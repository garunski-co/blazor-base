﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spent.Commons.Resources;

namespace Spent.Commons.Dtos.Todo;

[DtoResourceType(typeof(AppStrings))]
public class TodoItemDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = nameof(AppStrings.RequiredAttribute_ValidationError))]
    [Display(Name = nameof(AppStrings.Title))]
    public string? Title { get; set; }

    public DateTimeOffset Date { get; set; }

    public bool IsDone { get; set; }

    [JsonIgnore]
    public bool IsInEditMode { get; set; }
}
