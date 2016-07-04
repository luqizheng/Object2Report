namespace Coder.Object2Report
{
    public class ReportCell
    {
        public ReportCell(int maxCell)
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

        public int Index { get; internal set; }


        internal void NextRow()
        {
            RowIndex++;
        }
    }
}