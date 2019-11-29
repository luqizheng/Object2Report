using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DecimalColumnNullable<TEntity> : ExcelNumberNullableColumn<TEntity, decimal>
    {
        public DecimalColumnNullable(string name, Expression<Func<TEntity, decimal?>> action, bool isRequire = true) :
            base(name, action, isRequire)
        {
        }

        protected override string TypeName => "decimal";

        protected override bool TryConvertFromString(string strValue, out decimal? value)
        {
            var result = decimal.TryParse(strValue, out var val);
            value = val;
            return result;
        }

        protected override decimal? ConvertFromDouble(double d)
        {
            return Convert.ToDecimal(d);
        }
    }
}