using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int32Column<TEntity> : ExcelNumberColumn<TEntity, int>
    {
        public Int32Column(string name, Expression<Func<TEntity, int>> action, bool isRequire = true) : base(name,
            action, isRequire)
        {
        }


        protected override string TypeName => "Int32";

        protected override bool TryConvertFromString(string strValue, out int value)
        {
            return int.TryParse(strValue, out value);
        }

        protected override int ConvertFromDouble(double d)
        {
            return Convert.ToInt32(d);
        }
    }
}