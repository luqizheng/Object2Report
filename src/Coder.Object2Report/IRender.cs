namespace Coder.Object2Report
{
    /// <summary>
    /// </summary>
    public interface IRender
    {
        /// <summary>
        ///     Begin to write
        /// </summary>
        void OnReportWriting();

        /// <summary>
        ///     Begin to write header
        /// </summary>
        void OnHeaderWriting();

        /// <summary>
        ///     Writer Header Cell.
        /// </summary>
        /// <param name="cellCursor"></param>
        /// <param name="title"></param>
        /// <param name="format"></param>
        void WriteHeader<TObject>(CellCursor<TObject> cellCursor, string title, string format);

        /// <summary>
        /// </summary>
        void OnHeaderWrote();

        /// <summary>
        /// </summary>
        void OnBodyBuilding();

        /// <summary>
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        void WriteBodyCell<T, TObject>(CellCursor<TObject> currentPosition, T v, string format);

        /// <summary>
        /// </summary>
        void OnBodyBuilt();

        /// <summary>
        /// </summary>
        void OnFooterWriting();

        /// <summary>
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        void WriteFooterCell<T, TObject>(CellCursor<TObject> currentPosition, T v, string format);

        /// <summary>
        /// </summary>
        void OnFooterWrote();

        /// <summary>
        /// </summary>
        void OnReportWrote();

        /// <summary>
        /// </summary>
        void OnRowWrote();

        /// <summary>
        /// </summary>
        /// <param name="cellCursor"></param>
        /// <param name="rowIndex"></param>
        void OnRowWriting<TObject>(CellCursor<TObject> cellCursor, int rowIndex);
    }
}