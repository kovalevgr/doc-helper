using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DocHelper.Domain.Cache.Serializable
{
    [Serializable]
    public class TableRows
    {
        /// <summary>
        /// Rows of the table
        /// </summary>
        public IList<TableRow> Rows { get; } = new List<TableRow>();

        /// <summary>
        /// TableColumn's Info
        /// </summary>
        public IDictionary<int, TableColumnInfo> ColumnsInfo { get; }

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        public int FieldCount { get; set; }

        /// <summary>
        /// EFTableRows's unique ID
        /// </summary>
        public string TableName { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets a value that indicates whether the SqlDataReader contains one or more rows.
        /// </summary>
        public bool HasRows => Rows?.Count > 0;

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the Transact-SQL statement.
        /// </summary>
        public int RecordsAffected { get; } = -1;

        /// <summary>
        /// Gets the number of fields in the SqlDataReader that are not hidden.
        /// </summary>
        public int VisibleFieldCount { get; set; }

        /// <summary>
        /// Number of Db rows.
        /// </summary>
        public int RowsCount => Rows?.Count ?? 0;

        /// <summary>
        /// Gets or sets the Get(index)
        /// </summary>
        public TableRow this[int index]
        {
            get { return Get(index); }
            set { Rows[index] = value; }
        }

        /// <summary>
        /// TableRows structure
        /// </summary>
        public TableRows(DbDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            ColumnsInfo = new Dictionary<int, TableColumnInfo>(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                ColumnsInfo.Add(i, new TableColumnInfo
                {
                    Ordinal = i,
                    Name = reader.GetName(i),
                    DbTypeName = reader.GetDataTypeName(i),
                    TypeName = reader.GetFieldType(i)?.ToString()
                });
            }
        }

        /// <summary>
        /// TableRows structure
        /// </summary>
        public TableRows()
        {
            ColumnsInfo = new Dictionary<int, TableColumnInfo>();
        }

        /// <summary>
        /// Adds an item to the EFTableRows
        /// </summary>
        public void Add(TableRow item)
        {
            if (item != null)
            {
                Rows.Add(item);
            }
        }

        /// <summary>
        /// returns the value of the given index.
        /// </summary>
        public TableRow Get(int index) => Rows[index];

        /// <summary>
        /// Gets the column ordinal, given the name of the column.
        /// </summary>
        public int GetOrdinal(string name)
        {
            var keyValuePair =
                ColumnsInfo.FirstOrDefault(pair => pair.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (keyValuePair.Value != null)
            {
                return keyValuePair.Value.Ordinal;
            }

            throw new ArgumentOutOfRangeException(nameof(name), name);
        }

        /// <summary>
        /// Gets the name of the specified column.
        /// </summary>
        public string GetName(int ordinal) => GetColumnInfo(ordinal).Name;

        /// <summary>
        /// Gets a string representing the data type of the specified column.
        /// </summary>
        public string GetDataTypeName(int ordinal) => GetColumnInfo(ordinal).DbTypeName;

        /// <summary>
        /// Gets the Type that is the data type of the object.
        /// </summary>
        public Type GetFieldType(int ordinal) => Type.GetType(GetColumnInfo(ordinal).TypeName) ?? typeof(string);

        /// <summary>
        /// Gets the Type that is the data type of the object.
        /// </summary>
        public string GetFieldTypeName(int ordinal) => GetColumnInfo(ordinal).TypeName;

        private TableColumnInfo GetColumnInfo(int ordinal)
        {
            var dbColumnInfo = ColumnsInfo[ordinal];
            if (dbColumnInfo != null)
            {
                return dbColumnInfo;
            }

            throw new ArgumentOutOfRangeException(nameof(ordinal), $"Index[{ordinal}] was outside of array's bounds.");
        }
    }
}