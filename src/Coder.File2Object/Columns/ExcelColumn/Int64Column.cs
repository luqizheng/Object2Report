using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int64Column<TEntity> : ExcelNumberColumn<TEntity, long>
    {
        public Int64Column(string name, Expression<Func<TEntity, long>> action, bool isRequire = true) : base(name,
            action, isRequire)
        {
        }

        protected override string TypeName => "Int64";

        protected override bool TryConvertFromString(string strValue, out long value)
        {
            return long.TryParse(strValue, out value);
        }

        protected override long ConvertFromDouble(double d)
        {
            return Convert.ToInt64(d);
        }
    }
}