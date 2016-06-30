namespace Coder.Object2Report
{
    public interface IRender
    {
        /// <summary>
        /// </summary>
        void OnReportWritting();
        /// <summary>
        /// 
        /// </summary>
        void OnHeaderWritting();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        void WriteHeader(ReportCell currentPosition, object v);
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
        void WriteBodyCell(ReportCell currentPosition, object v, string format);
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
        void WriteFooterCell(ReportCell currentPosition, object v, string format);
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
        /// <param name="report"></param>
        /// <param name="rowIndex"></param>
        void OnRowWritting(Report report, int rowIndex);
    }
}