using System;
using System.Collections.Generic;
using Coder.Object2Report.Footers;
using Coder.Object2Report.Footers.Sum;

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
    }
    public static class FooterExtend
    {
        private static readonly Dictionary<Type, Func<FooterColumn>> SumFactory =
            new Dictionary<Type, Func<FooterColumn>>
            {
                {typeof(decimal), () => new DecimalColumn()},
                {typeof(int), () => new DecimalColumn()},
                {typeof(long), () => new DecimalColumn()},
                {typeof(float), () => new DecimalColumn()},
                {typeof(double), () => new DecimalColumn()}
            };

        private static readonly Dictionary<Type, Func<FooterColumn>> AvgFactory =
            new Dictionary<Type, Func<FooterColumn>>
            {
                {typeof(decimal), () => new DecimalColumn()},
                {typeof(int), () => new DecimalColumn()},
                {typeof(long), () => new DecimalColumn()},
                {typeof(float), () => new DecimalColumn()},
                {typeof(double), () => new DecimalColumn()}
            };

        public static FooterColumn Sum<T>(this IColumnResult<T> column)
            where T : new()
        {
            var result= SumFactory[typeof(T)]();
            column.Footer = result;
            return result;
        }

        public static FooterColumn Avg<T>(this IColumnResult<T> column)
            where T : new()
        {
            var result= AvgFactory[typeof(T)]();
            column.Footer = result;
            return result;
        }
    }
}