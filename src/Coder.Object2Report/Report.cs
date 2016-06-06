using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Coder.Object2Report
{
    public class Report<T>
    {
        private readonly bool _builderHeader;
        private readonly IList<IColumn> _columns;
        private readonly IRender _render;
        private int _cellIndex;
        private int _rowIndex;

        public Report(IRender render, bool builderHeader)
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));
            _render = render;
            _builderHeader = builderHeader;
            _columns = new List<IColumn>();
        }

        public Report<T> Column<TResult>(Expression<Func<T, TResult>> expression)
        {
            return Column(GetTilte(expression), expression);
        }


        public Report<T> Column<TResult>(string headerTitle, Expression<Func<T, TResult>> expression)
        {
            _columns.Add(new Column<T, TResult>(headerTitle, expression));
            return this;
        }

        public Report<T> Footer()
        {
            return this;
        }

        private string GetTilte<TResult>(Expression<Func<T, TResult>> expression)
        {
            switch (expression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpresion = (MemberExpression)expression.Body;
                    var attr = memberExpresion.Member.GetCustomAttribute<DisplayAttribute>();
                    return attr != null ? attr.GetName() : memberExpresion.Member.Name;
                default:
                    return expression.Name ?? "";
            }
        }

        public void Write(IEnumerable<T> data)
        {
            foreach (var item in data)
            {
                foreach (var col in _columns)
                {
                    _cellIndex = 0;
                    var obj = col.GetObject(item);
                    _render.Write(_cellIndex, _rowIndex, obj);
                    _cellIndex++;
                }
                _rowIndex++;
            }
        }

        public void WriteFooter()
        {
        }

        public void WriteHeader()
        {
        }
    }
}