#nullable enable
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using DocHelper.Domain.Cache.Key;
using DocHelper.Domain.Cache.Policy;
using DocHelper.Infrastructure.Cache.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Cache.Key
{
    public static class CacheKeyProvider
    {
        /// <summary>
        /// Gets query and returns its hashed key to store in the cache.
        /// </summary>
        /// <param name="command">The query.</param>
        /// <param name="context">DbContext is a combination of the Unit Of Work and Repository patterns.</param>
        /// <param name="cachePolicy">determines the Expiration time of the cache.</param>
        /// <returns>Information of the computed key of the input LINQ query.</returns>
        public static CacheKey GetKey(DbCommand command, DbContext context, CachePolicy cachePolicy)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (cachePolicy is null)
            {
                throw new ArgumentNullException(nameof(cachePolicy));
            }
            
            byte[] buffer = Encoding.UTF8.GetBytes(command.CommandText);
            var encodedString = Convert.ToBase64String(buffer);
            

            var cacheKey = GetCacheKey(command, cachePolicy.CacheSaltKey);
            var cacheKeyHash = $"{XxHashUnsafe.ComputeHash(encodedString):X}";
            var cacheDependencies = GetCacheDependencies(command, context, cachePolicy);

            return new CacheKey
            {
                Key = cacheKey,
                KeyHash = cacheKeyHash,
                CacheDependencies = cacheDependencies
            };
        }

        private static SortedSet<string> GetCacheDependencies(
            DbCommand command,
            DbContext context,
            CachePolicy cachePolicy)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            
            if (cachePolicy is null)
            {
                throw new ArgumentNullException(nameof(cachePolicy));
            }
            
            var tableNames = new SortedSet<string>(
                TableUtilities.GetTableNamesByContext(context).Select(x => x.TableName),
                StringComparer.OrdinalIgnoreCase);
            var textsInsideSquareBrackets = TableUtilities.GetSqlCommandTableNames(command.CommandText);
            var cacheDependencies = new SortedSet<string>(
                tableNames.Intersect(textsInsideSquareBrackets, StringComparer.OrdinalIgnoreCase),
                StringComparer.OrdinalIgnoreCase);
            if (cacheDependencies.Any())
            {
                return cacheDependencies;
            }

            return new SortedSet<string>();
        }

        private static string GetCacheKey(DbCommand command, string saltKey)
        {
            var cacheKey = new StringBuilder();

            foreach (DbParameter? parameter in command.Parameters)
            {
                if (parameter is null)
                {
                    continue;
                }

                cacheKey.Append(parameter.ParameterName)
                    .Append('=').Append(GetParameterValue(parameter)).Append(',')
                    .Append("Size").Append('=').Append(parameter.Size).Append(',')
                    .Append("Precision").Append('=').Append(parameter.Precision).Append(',')
                    .Append("Scale").Append('=').Append(parameter.Scale).Append(',')
                    .Append("Direction").Append('=').Append(parameter.Direction).Append(',');
            }

            cacheKey.AppendLine("SaltKey").Append('=').Append(saltKey);

            return cacheKey.ToString().Trim();
        }

        private static string GetParameterValue(DbParameter parameter)
        {
            switch (parameter.Value)
            {
                case DBNull:
                case null:
                    return String.Empty;
                case byte[] buffer:
                    return BytesToHex(buffer);
                default:
                    return parameter.Value?.ToString() ?? string.Empty;
            }
        }

        private static string BytesToHex(byte[] buffer)
        {
            var sb = new StringBuilder(buffer.Length * 2);
            foreach (var @byte in buffer)
            {
                sb.Append(@byte.ToString("X2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }
    }
}