using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensions
    {
        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, string>> action, bool isRequire = false)
        {
            manager.Add(new StringColumn<TEntity>(action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, long>> action)
        {
            manager.Add(new Int64Column<TEntity>(action));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, DateTime>> action)
        {
            manager.Add(new DateTimeColumn<TEntity>(action));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, decimal>> action)
        {
            manager.Add(new DecimalColumn<TEntity>(action));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, DateTimeOffset>> action)
        {
            manager.Add(new DateTimeOffsetColumn<TEntity>(action));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, int>> action)
        {
            manager.Add(new Int32Column<TEntity>(action));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert)
        {
            manager.Add(new CustomColumn<TEntity, TValue>(action, convert));
            return manager;
        }
    }
}