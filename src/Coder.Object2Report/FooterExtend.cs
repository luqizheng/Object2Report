using System;
using System.Linq.Expressions;
using Coder.Object2Report.Footers;
using Coder.Object2Report.Footers.Sum;

namespace Coder.Object2Report
{
    public static class FooterExtend
    {
        public static FooterColumn<T> Sum<T>(this IColumn<T> column, Expression<Func<T, decimal>> c) where T : new()
        {
            return new DecimalColumn<T>(c)
            {
                Index = column.Index
            };
        }

        public static FooterColumn<T> Sum<T>(this IColumn<T> column, Expression<Func<T, int>> c) where T : new()
        {
            return new Int32Column<T>(c)
            {
                Index = column.Index
            };
        }

        public static FooterColumn<T> Sum<T>(this IColumn<T> column, Expression<Func<T, long>> c) where T : new()
        {
            return new Int64Column<T>(c);
        }
    }
}