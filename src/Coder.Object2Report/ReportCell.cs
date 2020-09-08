namespace Coder.Object2Report
{
    /// <summary>
    /// </summary>
    public class CellCursor
    {
        /// <summary>
        /// </summary>
        /// <param name="maxCell"></param>
        public CellCursor(int maxCell)
        {
            MaxCell = maxCell;
        }

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

        /// <summary>
        /// </summary>
        internal void NextRow()
        {
            RowIndex++;
        }
    }
}