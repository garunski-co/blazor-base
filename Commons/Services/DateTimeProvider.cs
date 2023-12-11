using System;
using Spent.Commons.Services.Contracts;

namespace Spent.Commons.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset GetCurrentDateTime()
    {
        return DateTimeOffset.UtcNow;
    }
}
