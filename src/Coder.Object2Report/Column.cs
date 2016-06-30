using System;
using System.Linq.Expressions;
using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public abstract class Column : IColumn
    {
        public int ColSpan { get; set; }

        public int RowSpan { get; set; }

        public string Title { get; set; }

        public FooterCell Footer { get; set; }
        public abstract object GetValue(object item);

        public int Index { get; internal set; }
        public string Format { get; set; }
    }

    public class Column<T, TResult> : Column,
        IColumnFooterInfo<TResult>,
        IColumn<T> where T : new()
    {
        public Column(string title, Expression<Func<T, TResult>> itemExpression)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            Title = title;
            Expression = itemExpression;
            Func = itemExpression.Compile();
        }

        public Expression<Func<T, TResult>> Expression { get; }

        public Func<T, TResult> Func { get; set; }

        public object GetValue(T model)
        {
            var result = Func(model);
            return result;
        }

        object IColumn.GetValue(object o)
        {
            return GetValue((T) o);
        }

        public override object GetValue(object item)
        {
            return GetValue((T) item);
        }
    }
}