﻿using System;
using System.Collections.Generic;

namespace DocHelper.Domain.Cache.Serializable
{
    [Serializable]
    public class TableRow
    {
        /// <summary>
        /// An array of objects with the column values of the current row.
        /// </summary>
        public IList<object> Values { get; }

        /// <summary>
        /// Gets or sets a value that indicates the depth of nesting for the current row.
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        public int FieldCount => Values.Count;

        /// <summary>
        /// Returns Values[ordinal]
        /// </summary>
        public object this[int ordinal] => Values[ordinal];

        /// <summary>
        /// TableRow's structure
        /// </summary>
        public TableRow(IList<object> values)
        {
            Values = values;
        }
    }
}