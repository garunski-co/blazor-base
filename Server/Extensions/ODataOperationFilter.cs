﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Spent.Server.Extensions;

/// <summary>
/// https://docs.microsoft.com/en-us/odata/concepts/queryoptions-overview
/// </summary>
public class ODataOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

        var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

        if (descriptor is null)
            return;

        var odataQueryOptionsParameter =
            descriptor.Parameters.SingleOrDefault(p => typeof(ODataQueryOptions).IsAssignableFrom(p.ParameterType));

        if (descriptor.FilterDescriptors.Any(filter => filter.Filter is EnableQueryAttribute) ||
            odataQueryOptionsParameter is not null)
        {
            operation.Parameters.Add(new()
            {
                Name = "$select",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                },
                Description = "Returns only the selected properties. (ex. FirstName, LastName)",
                Required = false
            });

            operation.Parameters.Add(new()
            {
                Name = "$expand",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                },
                Description = "Include only the selected objects. (ex. Childrens, Locations)",
                Required = false
            });

            operation.Parameters.Add(new()
            {
                Name = "$filter",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                },
                Description = "Filter the response with OData filter queries.",
                Required = false
            });

            operation.Parameters.Add(new()
            {
                Name = "$search",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                },
                Description = "Filter the response with OData search queries.",
                Required = false
            });

            operation.Parameters.Add(new()
            {
                Name = "$top",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "int",
                },
                Description = "Number of objects to return. (ex. 25)",
                Required = false
            });

            operation.Parameters.Add(new()
            {
                Name = "$skip",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "int",
                },
                Description = "Number of objects to skip in the current order (ex. 50)",
                Required = false
            });

            operation.Parameters.Add(new()
            {
                Name = "$orderby",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                },
                Description = "Define the order by one or more fields (ex. LastModified)",
                Required = false
            });
        }

        if (odataQueryOptionsParameter is not null)
        {
            operation.Parameters.Remove(operation.Parameters.Single(p => p.Name == odataQueryOptionsParameter.Name));
        }
    }
}
