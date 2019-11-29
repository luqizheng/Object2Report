using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int64ColumnNullable<TEntity> : ExcelNumberNullableColumn<TEntity, long>
    {
        public Int64ColumnNullable(string name, Expression<Func<TEntity, long?>> action, bool isRequire = true) : base(
            name, action, isRequire)
        {
        }

        protected override string TypeName => "Int64";

        protected override bool TryConvertFromString(string strValue, out long? value)
        {
            var result = long.TryParse(strValue, out var re);
            value = re;
            return result;
        }

        protected override long? ConvertFromDouble(double d)
        {
            return Convert.ToInt64(d);
        }
    }
}