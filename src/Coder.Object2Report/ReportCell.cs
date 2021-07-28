using System.Collections;
using System.Collections.Generic;

namespace Coder.Object2Report
{
    /// <summary>
    /// </summary>
    public class CellCursor<TObject>
    {
        /// <summary>
        /// </summary>
        /// <param name="maxCell"></param>
        public CellCursor(IList<IColumn<TObject>> columns)
        {
            this.Columns = columns;
            MaxCell = columns.Count;
        }
        public IList<IColumn<TObject>> Columns { get; set; }
        /// <summary>
        ///     Get or set RowIndex
        /// </summary>
        public int RowIndex { get; internal set; }

        /// <summary>
        ///     Number of this Row.
        /// </summary>
        public int MaxCell { get; }

        /// <summary>
        /// </summary>
        public int Index { get; internal set; }

        public IColumn<TObject> Current => Columns[this.Index];

        /// <summary>
        /// </summary>
        internal void NextRow()
        {
            RowIndex++;
        }
    }
}