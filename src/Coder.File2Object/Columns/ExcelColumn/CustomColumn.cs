using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class CustomColumn<TEntity, TValue> : Column<TEntity, ICell, TValue>
    {
        private readonly Func<string, Tuple<TValue, string, bool>> _converTo;

        /// <summary>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="converTo">返回值item1为数值，item2为错误信息说明，item3 是否转换成功或者失败</param>
        public CustomColumn(Expression<Func<TEntity, TValue>> action,
            Func<string, Tuple<TValue, string, bool>> converTo) : base(action)
        {
            _converTo = converTo;
        }

        protected override bool TryConvert(ICell cell, out TValue val, out string errorMessage)
        {
            cell.SetCellType(CellType.String);
            var str = cell.StringCellValue;
            var result = _converTo(str);

            val = result.Item1;
            errorMessage = result.Item2;

            return result.Item3;
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入正确的值";
        }
    }
}