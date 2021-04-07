using System;
using DocHelper.Domain.Cache.Generator;
using DocHelper.Domain.Helpers;

namespace DocHelper.Domain.Cache.InMemory
{
    public class CacheEntry
    {
        public CacheEntry(string key, object value, DateTimeOffset expiresAt)
        {
            Key = key;
            Value = value;
            ExpiresAt = expiresAt;
        }

        public string Key { get; private set; }
        public object Value { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

        public T GetValue<T>(bool isDeepClone = true)
        {
            object val = Value;
              
            var t = typeof(T);

            if (t == TypeHelper.BoolType || t == TypeHelper.StringType || t == TypeHelper.CharType || t == TypeHelper.DateTimeType)
                return (T)Convert.ChangeType(val, t);

            if (t == TypeHelper.NullableBoolType || t == TypeHelper.NullableCharType || t == TypeHelper.NullableDateTimeType)
                return val == null ? default(T) : (T)Convert.ChangeType(val, Nullable.GetUnderlyingType(t));

            return DeepClonerGenerator.CloneObject<T>((T)val);
        }
    }
}