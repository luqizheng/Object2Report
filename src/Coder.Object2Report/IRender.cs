namespace Coder.Object2Report
{
    public interface IRender
    {
        /// <summary>
        /// 
        /// </summary>
        void OnWritting();

        void OnHeaderBuilding();
        void WriteHeader(ReportCell currentPosition, object v);
        void OnHeaderBuilt();

        void OnBodyBuilding();
        void WriteBodyCell(ReportCell currentPosition, object v, string format);
        void OnBodyBuilt();

        void OnFooterBuilding();
        void WriteFooterCell(ReportCell currentPosition, object v, string format);
        void OnFooterBuilt();

        void OnWrote();
    }
}