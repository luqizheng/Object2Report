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

        protected override bool TryConvert(ICell cell, out string val, out string errorMessage)
        {
            errorMessage = null;
            cell.SetCellType(CellType.String);
            val = cell.StringCellValue;
            return true;
        }

    }
}