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


        public override void SetValue(TEntity entity, TCell cell)
        {
            var val = Convert(cell);
            entity.SetPropertyValue(Action, val);
        }

        protected abstract TValue Convert(TCell cell);
    }

    public abstract class Column<TEntity, TCell>
    {
        public abstract void SetValue(TEntity entity, TCell cell);
    }
}