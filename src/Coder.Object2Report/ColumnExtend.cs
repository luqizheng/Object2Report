using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            var column = new Column<T, TResult>(headerTitle, expression);
            report.Columns.Add(column);
            column.Index = report.Columns.Count - 1;
            return column;
        }

        public static IColumnSetting<int> ColumnRowIndex<T>(this Report<T> report, string headerTitle)
        {
            if (headerTitle == null)
                throw new ArgumentNullException(nameof(headerTitle));

            var column = new ColumnIndex<T>(headerTitle);
            report.Columns.Add(column);

            return column;
        }


        public static IColumnSetting<TResult> Column<T, TResult>(this Report<T> report,
            Expression<Func<T, TResult>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var column = new Column<T, TResult>(GetTitle(expression), expression);
            report.Columns.Add(column);
            column.Index = report.Columns.Count - 1;
            return column;
        }


        private static string GetTitle<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            switch (expression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpresion = (MemberExpression)expression.Body;


                    foreach (var customer in memberExpresion.Member.GetCustomAttributes())
                    {
                        if (customer is DisplayAttribute displayAttribute)
                        {
                            return displayAttribute.Name;
                        }

                        if (customer is DisplayNameAttribute displayName)
                        {
                            return displayName.DisplayName;
                        }
                    }

                    return memberExpresion.Member.Name;


                default:
                    return expression.Name ?? "";
            }
        }
    }
}