using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public abstract class ExcelNumberColumn<TEntity, T> : Column<TEntity, ICell, T>
    {
        protected ExcelNumberColumn(string name, Expression<Func<TEntity, T>> action, bool isRequire = true) : base(
            name,
            action, isRequire)
        {
        }

        protected abstract string TypeName { get; }

        protected override bool TryConvert(ICell cell, out T val, out string errorMessage)
        {
            errorMessage = null;
            val = default;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    val = ConvertFromDouble(cell.NumericCellValue);
                    return true;
                default:

                    cell.SetCellType(CellType.String);
                    var result = TryConvertFromString(cell.StringCellValue?.Trim(), out val);
                    if (result == false) errorMessage = $"无法把{cell.StringCellValue}转化为有效的{TypeName}类型";

                    return result;
            }
        }

        protected abstract bool TryConvertFromString(string strValue, out T value);

        protected abstract T ConvertFromDouble(double d);

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入正确的{TypeName}类型";
        }
    }
}