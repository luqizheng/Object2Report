using System;
using System.Linq.Expressions;
using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public class ColumnIndex<T> : Column<T, int>
    {
        private int rowIndex = 1;

        public ColumnIndex(string title) : base(title, new Func<T, int>(t => 1))
        {
            Func = t => rowIndex;
        }

        public override void Write(T t, Action<CellCursor, object, string> action, CellCursor cellCursor)
        {
            base.Write(t, action, cellCursor);
            rowIndex++;
        }
    }

    public class Column<T, TResult>
        : IColumn<T>,
            IColumnSetting<TResult>
    {
        public Column(string title, Expression<Func<T, TResult>> itemExpression)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));

            Func = itemExpression.Compile();
        }

        public Column(string title, Func<T, TResult> func)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Func = func;
        }

        public Func<T, TResult> Func { get; set; }

        public string Title { get; }
        public int Index { get; internal set; }
        public string Format { get; set; }

        public virtual void Write(T t, Action<CellCursor, object, string> action, CellCursor cellCursor)
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