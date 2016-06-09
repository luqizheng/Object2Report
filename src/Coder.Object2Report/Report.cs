using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Coder.Object2Report
{
    public class Report<T>
        where T : new()
    {
        private readonly IList<IColumn<T>> _columns;

        private readonly IRender _render;
        private int _cellIndex;
        private int _rowIndex;

        public Report(IRender render)
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));
            _render = render;

            _columns = new List<IColumn<T>>();
        }

        public IColumnResult<TResult> Column<TResult>(Expression<Func<T, TResult>> expression)
        {
            return Column(GetTilte(expression), expression);
        }


        public IColumnResult<TResult> Column<TResult>(string headerTitle, Expression<Func<T, TResult>> expression)
        {
            var column = new Column<T, TResult>(headerTitle, expression);
            _columns.Add(column);
            column.Index = _columns.Count - 1;
            return column;
        }

        public void Render(IEnumerable<T> data)
        {
            _render.OnWritting();
            WriteHeader();
            WriteBody(data);
            WriteFooter();
            _render.OnWrote();
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
            _render.OnBodyBuilding();
            foreach (var item in data)
            {
                foreach (var col in _columns)
                {
                    _cellIndex = 0;
                    var obj = col.GetValue(item);
                    var currentPosition = GetCurrentCell();
                    _render.WriteBodyCell(currentPosition, obj, col.Format);
                    col.Footer?.Merge(obj);
                    _cellIndex++;
                }
                _rowIndex++;
            }
            _render.OnBodyBuilt();
        }

        public void WriteFooter()
        {
            _cellIndex = 0;

            foreach (var col in _columns)
            {
                var currentPosition = GetCurrentCell();
                var value = col.Footer != null ? col.Footer.GetValue() : "";

                _render.WriteFooterCell(currentPosition, value, col.Format);
                _cellIndex++;

            }
            _rowIndex++;
        }

        public void WriteHeader()
        {
            _render.OnHeaderBuilding();
            foreach (var col in _columns)
            {
                object title = col.Title;

                _render.WriteHeader(GetCurrentCell(), title);

                _cellIndex++;
            }
            _rowIndex++;
            _render.OnHeaderBuilt();
        }

        private Cell GetCurrentCell()
        {
            return new Cell
            {
                Index = _cellIndex,
                RowIndex = _rowIndex,
                MaxCell = _columns.Count
            };
        }
    }
}