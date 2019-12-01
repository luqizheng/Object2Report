using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Coder.File2Object
{
    internal static class PropertyHelper
    {
        public static PropertyInfo GetPropertyInfo<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            var memberSelectorExpression = expression.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property == null) throw new NotSupportedException("只支持属性表达式的");
                return property;
            }

            throw new NotSupportedException("只支持属性表达式的");
        }


        public static string GetPropertyNameFromDisplayName<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            var property = GetPropertyInfo(expression);

            DisplayNameAttribute attr = null;
            foreach (var att in property.GetCustomAttributes(typeof(DisplayNameAttribute)))
            {
                attr = att as DisplayNameAttribute;
                if (attr != null) return attr.DisplayName;
            }

            return property.Name;
        }

        public static void SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda,
            TValue value)
        {
            var property = GetPropertyInfo(memberLamda);

            property.SetValue(target, value);
        }
    }
}