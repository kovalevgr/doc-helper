﻿using System;

namespace DocHelper.Domain.Common.Entity
{
    public class TableEntityInfo
    {
        /// <summary>
        /// Gets the CLR class that is used to represent instances of this type.
        /// Returns null if the type does not have a corresponding CLR class (known as a shadow type).
        /// </summary>
        public Type ClrType { set; get; } = default!;

        /// <summary>
        /// The Corresponding tabe's name.
        /// </summary>
        public string TableName { set; get; } = default!;

        /// <summary>
        /// Debug info.
        /// </summary>
        public override string ToString() => $"{ClrType}::{TableName}";
    }
}