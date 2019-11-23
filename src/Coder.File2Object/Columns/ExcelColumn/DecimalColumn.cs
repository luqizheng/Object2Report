using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DecimalColumn<TEntity> : Column<TEntity, ICell, decimal>
    {
        public DecimalColumn(Expression<Func<TEntity, decimal>> action) : base(action)
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
                    var result = Decimal.TryParse(cell.StringCellValue, out val);
                    if (result == false)
                    {
                        errorMessage = $"无法把{cell.StringCellValue}转化为有效的数值类型";
                    }

                    return result;
            }
        }
    }
}