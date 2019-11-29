using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns
{
    public static class ColumnTemplateDefined
    {
        public const string ColumnName = "[column]";
    }

    public abstract class Column<TEntity, TCell, TValue> : Column<TEntity, TCell>
    {
        protected Column(string name, Expression<Func<TEntity, TValue>> action, bool isRequire = false)
        {
            Action = action;
            IsRequire = isRequire;
            Name = name;
        }

        public override string Name { get; protected set; }

        private Expression<Func<TEntity, TValue>> Action { get; }


        public override bool TrySetValue(TEntity entity, TCell cell, out string errorMessage1)
        {
            var result = TryConvert(cell, out var val, out errorMessage1);
            if (result)
                entity.SetPropertyValue(Action, val);

            return result;
        }

        public override void SetEmptyOrNull(TEntity entity)
        {
            var obj = default(TValue);
            entity.SetPropertyValue(Action, obj);
        }

        protected abstract bool TryConvert(TCell cell, out TValue val, out string errorMessage);
    }

    public abstract class Column<TEntity, TCell>
    {
        public bool IsRequire { get; set; }
        public abstract string Name { get; protected set; }

        public abstract bool TrySetValue(TEntity entity, TCell cell, out string errorMessage);
        public abstract void SetEmptyOrNull(TEntity entity);
        public abstract string GetErrorMessageIfEmpty();
    }
}