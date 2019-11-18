using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int64Column<TEntity> : Column<TEntity, ICell, long>
    {
        public Int64Column(Expression<Func<TEntity, long>> action) : base(action)
        {
        }

        protected override long Convert(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (long) cell.NumericCellValue;

                default:
                    cell.SetCellType(CellType.String);
                    return System.Convert.ToInt32(cell.StringCellValue);
            }
        }
    }
}