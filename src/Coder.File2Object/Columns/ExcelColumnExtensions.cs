using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensions
    {
        #region DateTimeOffset

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager, string name,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = true)
        {
            manager.Add(new DateTimeOffsetColumn<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager, string name,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            manager.Add(new DateTimeOffsetColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> ColumnDisplayNameAttribute<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> ColumnDisplayNameAttribute<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(name, action, isRequire);
        }
        #endregion

        #region Custome

        public static File2ObjectManager<TEntity, ICell>
            CustomColumn<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            manager.Add(new CustomColumn<TEntity, TValue>(name, action, convert, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            CustomColumn<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new CustomColumn<TEntity, TValue>(name, action, convert, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            CustomColumnDisplayAttribute<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            manager.Add(new CustomColumn<TEntity, TValue>(name, action, convert, isRequire));
            return manager;
        }

        #endregion

        #region enum

        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = true, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            manager.Add(new EnumColumn<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = true, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new EnumColumn<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            manager.Add(new EnumColumnNullable<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new EnumColumnNullable<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnumDisplayNameAttribute<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return ColumnEnum(manager, name, action, isRequire, fromDisplayAttribute);

        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnumDisplayNameAttribute<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return ColumnEnum(manager, name, action, isRequire, fromDisplayAttribute);
        }
        #endregion


        #region int

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, int>> action,
                bool isRequire = true)
        {
            manager.Add(new Int32Column<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, int>> action,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new Int32Column<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, int?>> action,
                bool isRequire = false)
        {
            manager.Add(new Int32ColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, int?>> action,
                bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new Int32ColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, int>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
           ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
               Expression<Func<TEntity, int?>> action,
               bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }
        #endregion

        #region String

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            manager.Add(new StringColumn<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }



        #endregion

        #region long

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, long>> action,
                bool isRequire = true)
        {
            manager.Add(new Int64Column<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, long>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, long?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, long>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, long?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }
        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, long?>> action,
                bool isRequire = false)
        {
            manager.Add(new Int64ColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }

        #endregion


        #region DateTime

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, DateTime>> action,
                bool isRequire = true)
        {
            manager.Add(new DateTimeColumn<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, DateTime?>> action,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new DateTimeColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, DateTime?>> action, bool isRequire = false)
        {
            manager.Add(new DateTimeColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, DateTime>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, DateTime>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, DateTime?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }

        #endregion

        #region decimal

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, decimal>> action,
                bool isRequire = true)
        {
            manager.Add(new DecimalColumn<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, decimal>> action,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new DecimalColumn<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, decimal?>> action,
                bool isRequire = false)
        {
            manager.Add(new DecimalColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, decimal?>> action,
                bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new DecimalColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }


        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, decimal?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, decimal>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }


        #endregion

        #region Boolean

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name, string trueStrExpress,
                Expression<Func<TEntity, bool>> action,
                bool isRequire = true)
        {
            manager.Add(new BooleanColumn<TEntity>(name, action, trueStrExpress, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name, string trueStrExpress,
                Expression<Func<TEntity, bool?>> action,
                bool isRequire = true)
        {
            manager.Add(new BooleanNullableColumn<TEntity>(name, action, trueStrExpress, isRequire));
            return manager;
        }


        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress, Expression<Func<TEntity, bool>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress, Expression<Func<TEntity, bool?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }
        #endregion
    }
}