using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DecimalColumn<TEntity> : ExcelNumberColumn<TEntity, decimal>
    {
        public DecimalColumn(string name, Expression<Func<TEntity, decimal>> action, bool isRequire = true) : base(name,
            action, isRequire)
        {
        }

        protected override string TypeName => "decimal";

        protected override bool TryConvertFromString(string strValue, out decimal value)
        {
            return decimal.TryParse(strValue, out value);
        }

        protected override decimal ConvertFromDouble(double d)
        {
            return Convert.ToDecimal(d);
        }
    }
}