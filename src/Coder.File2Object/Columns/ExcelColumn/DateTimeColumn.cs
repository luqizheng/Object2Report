using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DateTimeColumn<TEntity> : Column<TEntity, ICell, DateTime>
    {
        public DateTimeColumn(Expression<Func<TEntity, DateTime>> action) : base(action)
        {
        }

        protected override DateTime Convert(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell)) return cell.DateCellValue;

                    break;
            }

            cell.SetCellType(CellType.String);
            throw new ConvertException(cell.StringCellValue, typeof(DateTime));
        }
    }
}