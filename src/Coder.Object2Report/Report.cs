using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public class Report<T>
        where T : new()
    {
        private readonly IList<IColumn<T>> _columns;
        private readonly IList<IColumn<T>> _footerColumns;
        private readonly IRender _render;
        private int _cellIndex;
        private int _rowIndex;

        public Report(IRender render)
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));
            _render = render;

            _columns = new List<IColumn<T>>();
            _footerColumns = new List<IColumn<T>>();
        }

        public IColumn<T> Column<TResult>(Expression<Func<T, TResult>> expression)
        {
            return Column(GetTilte(expression), expression);
        }


        public IColumn<T> Column<TResult>(string headerTitle, Expression<Func<T, TResult>> expression)
        {
            var column = new Column<T, TResult>(headerTitle, expression);
            _columns.Add(column);
            column.Index = _columns.Count - 1;
            return column;
        }

        public void AddFooter(FooterColumn<T> column)
        {
            _footerColumns.Add(column);
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

        public void WriteBody(IEnumerable<T> data)
        {
            foreach (var item in data)
            {
                foreach (var col in _columns)
                {
                    _cellIndex = 0;
                    var obj = col.GetValue(item);
                    var currentPosition = new Point
                    {
                        Cell = _cellIndex,
                        Row = _rowIndex
                    };
                    _render.Write(currentPosition, obj);
                    _cellIndex++;
                }
                _rowIndex++;
            }
        }

        public void WriteFooter()
        {
            _cellIndex = 0;
            foreach (var col in _footerColumns)
            {
                if (_cellIndex == col.Index)
                {
                    object title = col.Title;
                    var currentPosition = new Point
                    {
                        Cell = _cellIndex,
                        Row = _rowIndex
                    };
                    _render.Write(currentPosition, title);
                }
                _cellIndex++;
                _rowIndex++;
            }
        }

        public void WriteHeader()
        {
            foreach (var col in _columns)
            {
                object title = col.Title;
                var currentPosition = new Point
                {
                    Cell = _cellIndex,
                    Row = _rowIndex
                };
                _render.Write(currentPosition, title);
                _rowIndex++;
                _cellIndex++;
            }
        }
    }
}