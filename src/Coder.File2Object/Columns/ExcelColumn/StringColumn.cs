using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class StringColumn<TEntity> : Column<TEntity, ICell, string>
    {
        public StringColumn(string name,Expression<Func<TEntity, string>> action, bool isRequire) : base(name, action, isRequire)
        {
        }

        protected override bool TryConvert(ICell cell, out string val, out string errorMessage)
        {
            errorMessage = null;
            cell.SetCellType(CellType.String);
            val = cell.StringCellValue.Trim();
            return true;
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入值";
        }
    }
}