using System;
using System.Net;
using Microsoft.Extensions.Localization;
using Spent.Commons.Resources;

namespace Spent.Commons.Exceptions;

public class UnauthorizedException : RestException
{
    public UnauthorizedException()
        : base(nameof(AppStrings.UnauthorizedException))
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public UnauthorizedException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public UnauthorizedException(LocalizedString message)
        : base(message)
    {
    }

    public UnauthorizedException(LocalizedString message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}
