using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int32Column<TEntity> : Column<TEntity, ICell, int>
    {
        public Int32Column(Expression<Func<TEntity, int>> action) : base(action)
        {
        }

        protected override int Convert(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (int) cell.NumericCellValue;

                default:
                    cell.SetCellType(CellType.String);
                    return System.Convert.ToInt32(cell.StringCellValue);
            }
        }
    }
}