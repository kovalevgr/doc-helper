using System;

namespace DocHelper.Infrastructure.Persistence.Exceptions
{
    public class SeedReferencesException : Exception
    {
        public SeedReferencesException(string exception)
            : base(exception)
        {
        }
    }
}