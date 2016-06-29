namespace Coder.Object2Report
{
    public interface IRender
    {
        /// <summary>
        /// 
        /// </summary>
        void OnReportWritting();

        void OnHeaderWritting();
        void WriteHeader(ReportCell currentPosition, object v);
        void OnHeaderWrote();

        void OnBodyBuilding();
        void WriteBodyCell(ReportCell currentPosition, object v, string format);
        void OnBodyBuilt();

        void OnFooterWritting();
        void WriteFooterCell(ReportCell currentPosition, object v, string format);
        void OnFooterWrote();

        void OnReportWrote();
        void OnRowWorte();
        void OnRowWritting(Report report, int rowIndex);
    }
}