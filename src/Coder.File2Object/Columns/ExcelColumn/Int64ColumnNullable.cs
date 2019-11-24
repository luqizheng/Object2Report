using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int64ColumnNullable<TEntity> : Column<TEntity, ICell, long?>
    {
        public Int64ColumnNullable(string name, Expression<Func<TEntity, long?>> action, bool isRequire = false) : base(name, action, isRequire)
        {
        }

        protected override bool TryConvert(ICell cell, out long? val, out string errorMessage)
        {
            errorMessage = null;
            val = null;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    val = (long?)cell.NumericCellValue;
                    return true;
                default:
                    cell.SetCellType(CellType.String);
                    var result = long.TryParse(cell.StringCellValue, out var valLong);

                    if (result == false) errorMessage = $"无法把{cell.StringCellValue}转化为有效的Int64类型";
                    else val = valLong;
                    return result;
            }
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入正确的int64类型";
        }

      
    }
}