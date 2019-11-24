using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;
using static Coder.File2Object.Columns.ColumnTemplateDefined;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DateTimeColumn<TEntity> : Column<TEntity, ICell, DateTime>
    {
        public DateTimeColumn(string name,Expression<Func<TEntity, DateTime>> action, bool isRequire = true) : base(name, action,
            isRequire)
        {
        }

        protected override bool TryConvert(ICell cell, out DateTime val, out string errorMessage)
        {
            errorMessage = null;
            val = default;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        val = cell.DateCellValue;
                        return true;
                    }

                    break;
            }

            cell.SetCellType(CellType.String);
            var valStr = cell.StringCellValue;
            var result = DateTime.TryParse(valStr, out val);

            if (result == false) errorMessage = $"无法把{valStr}转化为有效的日期类型";

            return result;
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnName}必须输入正确的日期类型";
        }
    }
}