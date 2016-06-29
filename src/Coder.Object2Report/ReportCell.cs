﻿namespace Coder.Object2Report
{
    public struct ReportCell
    {
        public IColumn Column { get; set; }
        /// <summary>
        ///     Get or set the cell index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     Get or set RowIndex
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        ///     Number of this Row.
        /// </summary>
        public int MaxCell { get; set; }
    }
}