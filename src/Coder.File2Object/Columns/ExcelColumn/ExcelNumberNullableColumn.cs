using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public abstract class ExcelNumberNullableColumn<TEntity, T> : ExcelNumberColumn<TEntity, T?>
        where T : struct


    {
        protected ExcelNumberNullableColumn(string name, Expression<Func<TEntity, T?>> action, bool isRequire = true) :
            base(name, action, isRequire)
        {
        }
    }
}