using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coder.Object2Report
{
    public interface IColumn
    {
        object GetObject(object t); string Title { get; set; }

    }
    public class Column<T, TResult> : IColumn
    {
        public Column(Expression<Func<T, TResult>> itemExpression)
        {
            
        }

        public Column(string title, Expression<Func<T, TResult>> itemExpression)
        {
            this.Title = title;
            this.Func = itemExpression.Compile();
        }
        
        public Func<T, TResult> Func { get; set; }

        public string Fromat { get; set; }

        public string Title { get; set; }

        public object GetObject(object t)
        {
            var result = Func((T)t);
            return result;
        }

    }

}
