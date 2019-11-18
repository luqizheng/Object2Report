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

        protected override decimal Convert(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (decimal) cell.NumericCellValue;

                default:
                    cell.SetCellType(CellType.String);
                    return System.Convert.ToDecimal(cell.StringCellValue);
            }
        }
    }
}