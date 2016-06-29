using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
namespace Coder.Object2Report
{
    public static class DataColumnExtend
    {
        public static IColumn<T> Format<T>(this IColumn<T> column, string format)
            where T : new()
        {
            column.Format = "{0:" + format + "}";
            return column;
        }

        public static IColumnFooterInfo<TResult> Column<T, TResult>(this Report<T> report, string headerTitle, Expression<Func<T, TResult>> expression)
            where T : new()
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

        public static IColumnFooterInfo<TResult> Column<T,TResult>(this Report<T> report, Expression<Func<T, TResult>> expression)
            where T : new()
        {
            return report.Column(GetTilte(expression), expression);
        }

        private static string GetTilte<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            switch (expression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpresion = (MemberExpression)expression.Body;
                    var attr = memberExpresion.Member.GetCustomAttributes<DisplayAttribute>().FirstOrDefault();
                    return attr != null ? ((DisplayAttribute)attr).Name : memberExpresion.Member.Name;
                default:
                    return expression.Name ?? "";
            }
        }
    }
}