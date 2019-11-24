using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int32ColumnNullable<TEntity> : Column<TEntity, ICell, int?>
    {
        public Int32ColumnNullable(Expression<Func<TEntity, int?>> action, bool isRequire = false) : base(action, isRequire)
        {
        }

        protected override bool TryConvert(ICell cell, out int? val, out string errorMessage)
        {

            errorMessage = null;

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    val = (int)cell.NumericCellValue;
                    return true;
                default:
                    cell.SetCellType(CellType.String);
                    var result = int.TryParse(cell.StringCellValue, out var intValu);
                    val = intValu;
                    if (result == false)
                    {
                        errorMessage = $"无法把{cell.StringCellValue}转化为有效的Int32类型";
                    }

                    return result;
            }
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入正确的int32类型";
        }
    }
}