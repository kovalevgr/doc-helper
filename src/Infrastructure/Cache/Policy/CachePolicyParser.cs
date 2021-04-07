using System;
using System.Globalization;
using System.Linq;
using DocHelper.Domain.Cache.Policy;
using JetBrains.Annotations;

namespace DocHelper.Infrastructure.Cache.Policy
{
    public static class CachePolicyParser
    {
        [CanBeNull]
        public static CachePolicy TryCreatePolicy(string commandText)
        {
            var commandTextLines = commandText.Split('\n');
            var cachePolicyCommentLine = commandTextLines.First(textLine => textLine.StartsWith(CachePolicy.Prefix, StringComparison.Ordinal)).Trim();

            var parts = cachePolicyCommentLine.Split(new[] { CachePolicy.PartsSeparator }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                return null;
            }

            var options = parts[1].Split(new[] { CachePolicy.ItemsSeparator }, StringSplitOptions.None);
            if (options.Length < 2)
            {
                return null;
            }

            if (!Enum.TryParse<CacheExpirationMode>(options[0], out var expirationMode))
            {
                return null;
            }

            if (!TimeSpan.TryParse(options[1], CultureInfo.InvariantCulture, out var timeout))
            {
                return null;
            }

            var saltKey = options.Length >= 3 ? options[2] : string.Empty;

            return new CachePolicy().ExpirationMode(expirationMode).SaltKey(saltKey).Timeout(timeout);
        }
    }
}