using System;

namespace DocHelper.Infrastructure.Cache.Utilities
{
    public static class StringUtilities
    {
        public static bool Contains(string text, string value)
        {
            return !string.IsNullOrWhiteSpace(text)
                   && text.Contains(value, StringComparison.Ordinal);
        }

        public static bool IsStringExist(string text, string[] seStrings)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                foreach (var marker in seStrings)
                {
                    if (line.Trim().StartsWith(marker, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}