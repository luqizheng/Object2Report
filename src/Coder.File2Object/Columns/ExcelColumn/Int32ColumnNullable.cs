using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int32ColumnNullable<TEntity> : ExcelNumberNullableColumn<TEntity, int>
    {
        public Int32ColumnNullable(string name, Expression<Func<TEntity, int?>> action, bool isRequire = true) : base(
            name, action, isRequire)
        {
        }

        protected override string TypeName => "Int32";

        protected override bool TryConvertFromString(string strValue, out int? value)
        {
            var result = int.TryParse(strValue, out var val);
            value = val;
            return result;
        }

        protected override int? ConvertFromDouble(double d)
        {
            return Convert.ToInt32(d);
        }
    }
}