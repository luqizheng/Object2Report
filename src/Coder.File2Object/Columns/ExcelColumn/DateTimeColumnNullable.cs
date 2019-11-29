using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DateTimeColumnNullable<TEntity> : Column<TEntity, ICell, DateTime?>
    {
        public DateTimeColumnNullable(string name, Expression<Func<TEntity, DateTime?>> action, bool isRequire = false)
            : base(name, action, isRequire)
        {
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入正确的日期类型";
        }

        protected override bool TryConvert(ICell cell, out DateTime? valueDateTime, out string errorMessage)
        {
            errorMessage = null;
            valueDateTime = default;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        valueDateTime = cell.DateCellValue;
                        return true;
                    }

                    break;
            }

            DateTime val;
            cell.SetCellType(CellType.String);
            var valStr = cell.StringCellValue;
            var result = DateTime.TryParse(valStr, out val);

            if (result == false) errorMessage = $"无法把{valStr}转化为有效的日期类型";
            else
                valueDateTime = val;
            return result;
        }
    }
}