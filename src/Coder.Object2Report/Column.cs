using System;
using System.Linq.Expressions;
using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public class Column<T, TResult>
        : IColumn<T>,
            IColumnSetting<TResult>
    {
        public Column(string title, Expression<Func<T, TResult>> itemExpression)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            Title = title;

            Func = itemExpression.Compile();
        }

        public Column(string title, Func<T, TResult> func)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            Title = title;
            Func = func;
        }

        public Func<T, TResult> Func { get; set; }

        public string Title { get; }
        public int Index { get; internal set; }
        public string Format { get; set; }

        public void Write(T t, Action<CellCursor, object, string> action, CellCursor cellCursor)
        {
            var value = Func(t);
            action(cellCursor, value, Format);
            Footer?.Calculate(value);
        }

        public void WriteFooter(Action<CellCursor, object, string> action, CellCursor cellCursor)
        {
            Footer?.Write(action, cellCursor);
        }

        public FooterCell<TResult> Footer { get; set; }
    }
}