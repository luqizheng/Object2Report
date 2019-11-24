using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DecimalColumnNullable<TEntity> : Column<TEntity, ICell, decimal?>
    {
        public DecimalColumnNullable(Expression<Func<TEntity, decimal?>> action, bool isRequire = false) : base(action, isRequire)
        {
        }

        protected override bool TryConvert(ICell cell, out decimal? valReturn, out string errorMessage)
        {
            errorMessage = null;

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    valReturn = (decimal)cell.NumericCellValue;
                    return true;
                default:
                    decimal val = 0;
                    cell.SetCellType(CellType.String);
                    var result = decimal.TryParse(cell.StringCellValue, out val);
                    if (result == false) errorMessage = $"无法把{cell.StringCellValue}转化为有效的数值类型";

                    valReturn = val;
                    return result;
            }
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入正确的decimal类型";
        }
    }
}