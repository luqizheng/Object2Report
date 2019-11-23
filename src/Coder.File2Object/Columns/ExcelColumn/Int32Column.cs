﻿using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class Int32Column<TEntity> : Column<TEntity, ICell, int>
    {
        public Int32Column(Expression<Func<TEntity, int>> action) : base(action)
        {
        }

        protected override bool TryConvert(ICell cell, out int val, out string errorMessage)
        {

            errorMessage = null;
            val = 0;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    val = (int)cell.NumericCellValue;
                    return true;
                default:
                    cell.SetCellType(CellType.String);
                    var result = int.TryParse(cell.StringCellValue, out val);
                    if (result == false)
                    {
                        errorMessage = $"无法把{cell.StringCellValue}转化为有效的Int32类型";
                    }

                    return result;
            }
        }
    }
}