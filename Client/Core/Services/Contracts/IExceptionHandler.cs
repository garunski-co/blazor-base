using System;
using System.Collections.Generic;

namespace Spent.Client.Core.Services.Contracts;

public interface IExceptionHandler
{
    void Handle(Exception exception, IDictionary<string, object>? parameters = null);
}
