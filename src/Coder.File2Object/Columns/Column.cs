using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns
{
    public abstract class Column<TEntity, TCell, TValue> : Column<TEntity, TCell>
    {
        protected Column(Expression<Func<TEntity, TValue>> action)
        {
            Action = action;
        }

        private Expression<Func<TEntity, TValue>> Action { get; }


        public override bool TrySetValue(TEntity entity, TCell cell, out string errorMessage1)
        {
            var result = TryConvert(cell, out var val, out errorMessage1);
            if (result)
                entity.SetPropertyValue(Action, val);

            return result;
        }

        protected abstract bool TryConvert(TCell cell, out TValue val, out string errorMessage);
    }

    public abstract class Column<TEntity, TCell>
    {
        public abstract bool TrySetValue(TEntity entity, TCell cell, out string errorMessage);
    }
}