using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class EnumColumn<TEntity, TEnum> : Column<TEntity, ICell, TEnum> where
        TEnum : struct
    {
        private readonly bool _fromDisplayAttribute;

        public EnumColumn(string name, Expression<Func<TEntity, TEnum>> action, bool isRequire = false,
            bool fromDisplayAttribute = false) : base(name, action, isRequire)
        {
            _fromDisplayAttribute = fromDisplayAttribute;
        }

        public override string GetErrorMessageIfEmpty()
        {
            return ColumnTemplateDefined.ColumnName + "必须输入值";
        }

        protected override bool TryConvert(ICell cell, out TEnum val, out string errorMessage)
        {
            errorMessage = null;
            bool convertResult;
            val = default;
            if (_fromDisplayAttribute)
            {
                convertResult = EnumHelper.TryFromDisplayName(cell.StringCellValue, out val);
            }
            else
            {
                convertResult = Enum.TryParse(typeof(TEnum), cell.StringCellValue, out var valConvert);
                if (convertResult)
                    val = (TEnum) valConvert;
            }

            if (!convertResult)
                errorMessage = ColumnTemplateDefined.ColumnName + "列中的值" + cell.StringCellValue + "无法是被为有效的" +
                               typeof(TEnum).Name + "值";

            return convertResult;
        }
    }
}