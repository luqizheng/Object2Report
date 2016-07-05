namespace Coder.Object2Report
{
    public abstract class RenderBase : IRender
    {
        public virtual void OnReportWritting()
        {
        }

        public virtual void OnHeaderWritting()
        {
        }

        public abstract void WriteHeader(ReportCell currentPosition, string title, string format);


        public virtual void OnHeaderWrote()
        {
        }

        public virtual void OnBodyBuilding()
        {
        }

        public abstract void WriteBodyCell<T>(ReportCell currentPosition, T v, string format);


        public virtual void OnBodyBuilt()
        {
        }

        public virtual void OnFooterWritting()
        {
        }

        public abstract void WriteFooterCell<T>(ReportCell currentPosition, T v, string format);


        public virtual void OnFooterWrote()
        {
        }

        public virtual void OnReportWrote()
        {
        }

        public virtual void OnRowWorte()
        {
        }

        public virtual void OnRowWritting(ReportCell cell, int rowIndex)
        {
        }
    }
}