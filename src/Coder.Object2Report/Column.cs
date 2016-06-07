using System;
using System.Linq.Expressions;

namespace Coder.Object2Report
{
    public class Column<T, TResult> : IColumn<T> where T : new()
    {
        private readonly Expression<Func<T, TResult>> _itemExpression;


        public Column(string title, Expression<Func<T, TResult>> itemExpression)
        {
            _itemExpression = itemExpression;
            if (title == null) throw new ArgumentNullException(nameof(title));
            Title = title;
            Func = itemExpression.Compile();
        }

        public Func<T, TResult> Func { get; set; }

        public string Fromat { get; set; }

        public string Title { get; set; }

        public object GetValue(T t)
        {
            var result = Func(t);
            return result;
        }

        public int Index { get; set; }
    }
}