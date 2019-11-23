﻿using System;
using System.Linq.Expressions;

namespace Coder.File2Object.Columns
{
    public static class ColumnTemplateDefined
    {
        public const string ColumnName = "[column]";
    }

    public abstract class Column<TEntity, TCell, TValue> : Column<TEntity, TCell>
    {
        protected Column(Expression<Func<TEntity, TValue>> action, bool isRequire = false)
        {
            Action = action;
            IsRequire = isRequire;
        }

        private Expression<Func<TEntity, TValue>> Action { get; }


        public override bool TrySetValue(TEntity entity, TCell cell, out string errorMessage1)
        {
            if (IsRequire)
            {
                errorMessage1 = GetErrorMessageIfEmpty();
                return false;
            }

            var result = TryConvert(cell, out var val, out errorMessage1);
            if (result)
                entity.SetPropertyValue(Action, val);

            return result;
        }

        protected abstract bool TryConvert(TCell cell, out TValue val, out string errorMessage);
    }

    public abstract class Column<TEntity, TCell>
    {
        public bool IsRequire { get; set; }
        public abstract bool TrySetValue(TEntity entity, TCell cell, out string errorMessage);
        public abstract string GetErrorMessageIfEmpty();
    }
}