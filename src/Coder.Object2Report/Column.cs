using System;
using System.Linq.Expressions;
using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public class Column<T, TResult> :
        IColumnResult<TResult>,
        IColumn<T> where T : new()
    {
        public Expression<Func<T, TResult>> Expression { get; }
        private string _format;

        public Column(string title, Expression<Func<T, TResult>> itemExpression)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            Title = title;
            Expression = itemExpression;
            Func = itemExpression.Compile();
        }

        public Func<T, TResult> Func { get; set; }

        public int ColSpan { get; set; }

        public int RowSpan { get; set; }

        public string Title { get; set; }

        public object GetValue(T t)
        {
            var result = Func(t);
            return result;
        }

        public FooterColumn Footer { get; set; }

        public int Index { get; internal set; }
        public string Format
        {
            get
            {
                return _format ?? (_format = "{0}");
            }
            set
            {
                _format = value;
            }
        }
    }
}