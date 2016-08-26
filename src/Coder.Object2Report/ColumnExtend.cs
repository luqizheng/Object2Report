using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Coder.Object2Report
{
    public static class ColumnExtend
    {
        /// <summary>
        ///     Refer https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx
        ///     Refer https://msdn.microsoft.com/en-us/library/0c899ak8(v=vs.110).aspx
        /// </summary>
        public static IColumnSetting<T> Format<T>(this IColumnSetting<T> column, string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));
            column.Format = format;
            return column;
        }

        public static IColumnSetting<TResult> Column<T, TResult>(this Report<T> report, string headerTitle,
            Func<T, TResult> expression)
        {
            if (headerTitle == null)
                throw new ArgumentNullException(nameof(headerTitle));
            if (headerTitle == null)
                throw new ArgumentNullException(nameof(expression));
            var column = new Column<T, TResult>(headerTitle, expression);
            report.Columns.Add(column);
            column.Index = report.Columns.Count - 1;
            return column;
        }

        public static IColumnSetting<TResult> Column<T, TResult>(this Report<T> report,
            Expression<Func<T, TResult>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var column = new Column<T, TResult>(GetTilte(expression), expression);
            report.Columns.Add(column);
            column.Index = report.Columns.Count - 1;
            return column;
        }

        private static string GetTilte<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            switch (expression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpresion = (MemberExpression)expression.Body;
                    DisplayAttribute attr = null;
#if NET40 || NET461
                    var attrs = memberExpresion.Member.GetCustomAttributes(typeof(DisplayAttribute), true);
                    attr = attrs.Length == 0 ? null : (DisplayAttribute) attrs[0];
                    return attr != null ? attr.Name : memberExpresion.Member.Name;
#else
                    var attrData = (from item in memberExpresion.Member.CustomAttributes
                                    where item.AttributeType == typeof(DisplayAttribute)
                                    select item).FirstOrDefault();
                    if (attrData != null && attrData.NamedArguments.Count() > 0)
                    {
                        return attrData.NamedArguments[0].TypedValue.Value.ToString();
                    }
                    return memberExpresion.Member.Name;
#endif

                default:
                    return expression.Name ?? "";
            }
        }
    }
}