using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class BooleanColumn<TEntity> : Column<TEntity, ICell, bool>
    {
        private readonly string _trueStrExpress;

        public BooleanColumn(string name, Expression<Func<TEntity, bool>> action, string trueStrExpress, bool isRequire = false) : base(name, action, isRequire)
        {
            _trueStrExpress = trueStrExpress;
        }

        public override string GetErrorMessageIfEmpty()
        {
            return "请输入值";
        }

        protected override bool TryConvert(ICell cell, out bool val, out string errorMessage)
        {
            errorMessage = null;
            val = cell.StringCellValue == _trueStrExpress;
            return true;
        }
    }
}