using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensions
    {
        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            manager.Add(new StringColumn<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, long>> action,
                bool isRequire = true)
        {
            manager.Add(new Int64Column<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, long?>> action,
                bool isRequire = false)
        {
            manager.Add(new Int64ColumnNullable<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, DateTime>> action,
                bool isRequire = true)
        {
            manager.Add(new DateTimeColumn<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name,
                Expression<Func<TEntity, DateTime?>> action, bool isRequire = false)
        {
            manager.Add(new DateTimeColumnNullable<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, decimal>> action,
                bool isRequire = true)
        {
            manager.Add(new DecimalColumn<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, decimal?>> action,
                bool isRequire = false)
        {
            manager.Add(new DecimalColumnNullable<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,string name,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = true)
        {
            manager.Add(new DateTimeOffsetColumn<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,string name,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            manager.Add(new DateTimeOffsetColumnNullable<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, int>> action,
                bool isRequire = true)
        {
            manager.Add(new Int32Column<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,string name, Expression<Func<TEntity, int?>> action,
                bool isRequire = false)
        {
            manager.Add(new Int32ColumnNullable<TEntity>(name,action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager,string name,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            manager.Add(new CustomColumn<TEntity, TValue>(name,action, convert, isRequire));
            return manager;
        }


        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,string name,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = true, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            manager.Add(new EnumColumn<TEntity, TEnum>(name,action, isRequire, fromDisplayAttribute));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,string name,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            manager.Add(new EnumColumnNullable<TEntity, TEnum>(name,action, isRequire, fromDisplayAttribute));
            return manager;
        }
    }
}