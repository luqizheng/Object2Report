using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DateTimeOffsetColumn<TEntity> : Column<TEntity, ICell, DateTimeOffset>
    {
        public DateTimeOffsetColumn(Expression<Func<TEntity, DateTimeOffset>> action) : base(action)
        {
        }

        protected override DateTimeOffset Convert(ICell cell)
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