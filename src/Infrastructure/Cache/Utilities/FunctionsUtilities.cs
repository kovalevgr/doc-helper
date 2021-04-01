using System;
using System.Collections.Generic;
using System.Linq;

namespace DocHelper.Infrastructure.Cache.Utilities
{
    public static class FunctionsUtilities
    {
        private static readonly HashSet<string> NonDeterministicFunctions = new(StringComparer.OrdinalIgnoreCase)
        {
            "NEWID()",
            "GETDATE()",
            "GETUTCDATE()",
            "SYSDATETIME()",
            "SYSUTCDATETIME()",
            "SYSDATETIMEOFFSET()",
            "CURRENT_USER()",
            "CURRENT_TIMESTAMP()",
            "HOST_NAME()",
            "USER_NAME()",
            "NOW()",
            "getguid()",
            "uuid_generate_v4()",
            "current_timestamp",
            "current_date",
            "current_time"
        };

        public static bool ContainsNonDeterministicFunction(string text) =>
            NonDeterministicFunctions.Any(item =>
                text.Contains(item, StringComparison.OrdinalIgnoreCase));
    }
}