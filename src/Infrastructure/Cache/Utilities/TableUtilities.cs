using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DocHelper.Domain.Common.Entity;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Cache.Utilities
{
    public static class TableUtilities
    {
        private static readonly ConcurrentDictionary<Type, Lazy<List<TableEntityInfo>>> ContextTableNames =
            new ConcurrentDictionary<Type, Lazy<List<TableEntityInfo>>>();

        private static readonly ConcurrentDictionary<string, Lazy<SortedSet<string>>> CommandTableNames =
            new ConcurrentDictionary<string, Lazy<SortedSet<string>>>(StringComparer.OrdinalIgnoreCase);

        public static IList<TableEntityInfo> GetTableNamesByContext(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return ContextTableNames.GetOrAdd(context.GetType(),
                _ => new Lazy<List<TableEntityInfo>>(() =>
                    {
                        var tableNames = new List<TableEntityInfo>();
                        foreach (var entityType in context.Model.GetEntityTypes())
                        {
                            tableNames.Add(
                                new TableEntityInfo
                                {
                                    ClrType = entityType.ClrType,
                                    TableName = entityType.GetTableName()
                                });
                        }

                        return tableNames;
                    },
                    LazyThreadSafetyMode.ExecutionAndPublication)).Value;
        }

        /// <summary>
        /// Extracts the table names of an SQL command.
        /// </summary>
        public static SortedSet<string> GetSqlCommandTableNames(string commandText)
        {
            var commandTextKey = $"{HashUtilities.ComputeHash(commandText)}";
            return CommandTableNames.GetOrAdd(commandTextKey,
                _ => new Lazy<SortedSet<string>>(() => GetRawSqlCommandTableNames(commandText),
                    LazyThreadSafetyMode.ExecutionAndPublication)).Value;
        }

        private static SortedSet<string> GetRawSqlCommandTableNames(string commandText)
        {
            string[] tableMarkers = {"FROM", "JOIN", "INTO", "UPDATE"};

            var tables = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            var sqlItems = commandText.Split(new[] {" ", "\r\n", Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);
            var sqlItemsLength = sqlItems.Length;
            for (var i = 0; i < sqlItemsLength; i++)
            {
                foreach (var marker in tableMarkers)
                {
                    if (!sqlItems[i].Equals(marker, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    ++i;
                    if (i >= sqlItemsLength)
                    {
                        continue;
                    }

                    var tableName = string.Empty;

                    var tableNameParts = sqlItems[i].Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries);
                    if (tableNameParts.Length == 1)
                    {
                        tableName = tableNameParts[0].Trim();
                    }
                    else if (tableNameParts.Length >= 2)
                    {
                        tableName = tableNameParts[1].Trim();
                    }

                    if (string.IsNullOrWhiteSpace(tableName))
                    {
                        continue;
                    }

                    tableName = tableName.Replace("[", "", StringComparison.Ordinal)
                        .Replace("]", "", StringComparison.Ordinal)
                        .Replace("'", "", StringComparison.Ordinal)
                        .Replace("`", "", StringComparison.Ordinal)
                        .Replace("\"", "", StringComparison.Ordinal);
                    tables.Add(tableName);
                }
            }

            return tables;
        }
    }
}