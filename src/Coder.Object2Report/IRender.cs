namespace Coder.Object2Report
{
    public interface IRender
    {
        /// <summary>
        /// 
        /// </summary>
        void OnWritting();

        void OnHeaderBuilding();
        void WriteHeader(Cell currentPosition, object v);
        void OnHeaderBuilt();

        void OnBodyBuilding();
        void WriteBodyCell(Cell currentPosition, object v, string format);
        void OnBodyBuilt();

        void OnFooterBuilding();
        void WriteFooterCell(Cell currentPosition, object v, string format);
        void OnFooterBuilt();

        void OnWrote();
    }
}