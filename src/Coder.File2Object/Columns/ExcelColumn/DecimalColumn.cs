using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DecimalColumn<TEntity> : Column<TEntity, ICell, decimal>
    {
        public DecimalColumn(string name,Expression<Func<TEntity, decimal>> action, bool isRequire = true) : base(name, action, isRequire)
        {
        }

        protected override bool TryConvert(ICell cell, out decimal val, out string errorMessage)
        {
            errorMessage = null;
            val = 0;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    val = (decimal)cell.NumericCellValue;
                    return true;
                default:
                    cell.SetCellType(CellType.String);
                    var result = decimal.TryParse(cell.StringCellValue, out val);
                    if (result == false) errorMessage = $"无法把{cell.StringCellValue}转化为有效的数值类型";

                    return result;
            }
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入正确的decimal类型";
        }
    }
}