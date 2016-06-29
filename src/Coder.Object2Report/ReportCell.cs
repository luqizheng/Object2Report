namespace Coder.Object2Report
{
    public class ReportCell
    {
        private readonly Report _report;

        public ReportCell(Report report)
        {
            _report = report;
        }

        public IColumn Column { get; internal set; }
        /// <summary>
        ///     Get or set the cell index
        /// </summary>
        public int Index => this.Column?.Index ?? -1;

        /// <summary>
        ///     Get or set RowIndex
        /// </summary>
        public int RowIndex { get; internal set; }

        /// <summary>
        ///     Number of this Row.
        /// </summary>
        public int MaxCell => this._report.Columns.Count;


        internal void SetCell(IColumn column)
        {
            this.Column = column;
        }

        internal void NextRow()
        {
            this.RowIndex++;
            this.Column = null;
        }

    }
}