using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Coder.File2Object
{
    internal static class EnumHelper
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<object, string>> displayNameCache =
            new ConcurrentDictionary<Type, ConcurrentDictionary<object, string>>();

        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, object>> enumCache =
            new ConcurrentDictionary<Type, ConcurrentDictionary<string, object>>();

        public static bool TryFromDisplayName<T>(string displayName, out T value)
            where T : struct
        {
            if (enumCache.ContainsKey(typeof(T)))
            {
                IDictionary<string, object> direct = enumCache[typeof(T)];
                if (direct.ContainsKey(displayName))
                    value = (T) direct[displayName];
                else
                    return Enum.TryParse(displayName, true, out value);

                return true;
            }

            BuildCache(typeof(T));
            return TryFromDisplayName(displayName, out value);
        }

        private static void BuildCache(Type type)
        {
            foreach (var obj in Enum.GetValues(type))
            {
                var fieldInfo = type.GetField(obj.ToString());
                var display = GetStringFromAttr(fieldInfo);
                displayNameCache.AddOrUpdate(type, key =>
                {
                    var c = new ConcurrentDictionary<object, string>();
                    c.TryAdd(obj, display);
                    return c;
                }, (key, updateDirect) =>
                {
                    updateDirect.TryAdd(obj, display);
                    return updateDirect;
                });


                enumCache.AddOrUpdate(type, key =>
                {
                    var c = new ConcurrentDictionary<string, object>();
                    c.TryAdd(display, obj);
                    return c;
                }, (key, updateDirect) =>
                {
                    updateDirect.TryAdd(display, obj);
                    return updateDirect;
                });
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="fieldinfo"></param>
        /// <returns></returns>
        private static string GetStringFromAttr(FieldInfo fieldinfo)
        {
            var attrs = fieldinfo.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs.Length != 0)
            {
                var attr = (DisplayAttribute) attrs[0];
                return attr.Name;
            }

            return fieldinfo.GetValue(null).ToString();
        }

        /// <summary>
        ///     Define the enum display name as follows.
        ///     public enum MyEnum
        ///     {
        ///     [Display(Name = "None")]
        ///     None,
        ///     [Display(Name = "Item1")]
        ///     Item1,
        ///     [Display(Name = "Item2")]
        ///     Item2
        ///     }
        /// </summary>
        public static string GetEnumDisplayName(this Enum value)
        {
            var key = value.GetType();
            if (displayNameCache.ContainsKey(key))
            {
                var direct = displayNameCache[key];
                return direct[value];
            }

            BuildCache(value.GetType());
            return GetEnumDisplayName(value);
        }
    }
}