using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Coder.File2Object
{
    internal static class LambdaExtensions
    {
        public static void SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda,
            TValue value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;

                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, object>> memberLamda, object value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;

                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, int>> memberLamda, int value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, long>> memberLamda, long value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, DateTime>> memberLamda, DateTime value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, DateTime?>> memberLamda,
            DateTime? value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, DateTimeOffset>> memberLamda,
            DateTimeOffset value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, DateTimeOffset?>> memberLamda,
            DateTimeOffset? value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, float?>> memberLamda, float? value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, float>> memberLamda, float value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, decimal?>> memberLamda, decimal? value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }

        public static void SetPropertyValue<T>(this T target, Expression<Func<T, decimal>> memberLamda, decimal value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null) property.SetValue(target, value);
            }
        }
    }
}