using System;
using DocHelper.Domain.Cache.Serializable;
using JetBrains.Annotations;

namespace DocHelper.Domain.Cache
{
    [Serializable]
    public class CachedData
    {
        /// <summary>
        /// DbDataReader's result.
        /// </summary>
        [CanBeNull] public TableRows TableRows { get; set; }

        /// <summary>
        /// DbDataReader's NonQuery result.
        /// </summary>
        public int NonQuery { get; set; }

        /// <summary>
        /// DbDataReader's Scalar result.
        /// </summary>
        [CanBeNull] public object Scalar { get; set; }

        /// <summary>
        /// Is result of the query null?
        /// </summary>
        public bool IsNull { get; set; }
    }
}