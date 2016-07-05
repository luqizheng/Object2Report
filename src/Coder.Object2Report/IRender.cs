namespace Coder.Object2Report
{
    public interface IRender
    {
        /// <summary>
        /// Begin to write
        /// </summary>
        void OnReportWritting();
        /// <summary>
        /// Begin to write header
        /// </summary>
        void OnHeaderWritting();

        /// <summary>
        /// Writer Header Cell.
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="title"></param>
        /// <param name="format"></param>
        void WriteHeader(ReportCell currentPosition, string title, string format);
        /// <summary>
        /// 
        /// </summary>
        void OnHeaderWrote();
        /// <summary>
        /// 
        /// </summary>
        void OnBodyBuilding();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        void WriteBodyCell<T>(ReportCell currentPosition, T v, string format);
        /// <summary>
        /// 
        /// </summary>
        void OnBodyBuilt();
        /// <summary>
        /// 
        /// </summary>
        void OnFooterWritting();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        void WriteFooterCell<T>(ReportCell currentPosition, T v, string format);
        /// <summary>
        /// 
        /// </summary>
        void OnFooterWrote();
        /// <summary>
        /// 
        /// </summary>
        void OnReportWrote();
        /// <summary>
        /// 
        /// </summary>
        void OnRowWorte();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="rowIndex"></param>
        void OnRowWritting(ReportCell cell, int rowIndex);
    }
}