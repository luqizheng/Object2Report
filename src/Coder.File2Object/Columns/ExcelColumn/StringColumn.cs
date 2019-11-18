using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class StringColumn<TEntity> : Column<TEntity, ICell, string>
    {
        public StringColumn(Expression<Func<TEntity, string>> action) : base(action)
        {
        }

        protected override string Convert(ICell cell)
        {
            cell.SetCellType(CellType.String);
            return cell.StringCellValue;
        }
    }

    public class CustomColumn<TEntity, TValue> : Column<TEntity, ICell, TValue>
    {
        private readonly Func<string, TValue> _converTo;

        public CustomColumn(Expression<Func<TEntity, TValue>> action, Func<string, TValue> converTo) : base(action)
        {
            _converTo = converTo;
        }

        protected override TValue Convert(ICell cell)
        {
            cell.SetCellType(CellType.String);
            var str = cell.StringCellValue;
            return _converTo(str);
        }
    }
}