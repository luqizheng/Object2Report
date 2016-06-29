namespace Coder.Object2Report
{
    public abstract class RenderBase : IRender
    {
        public virtual void OnWritting()
        {
        }

        public virtual void OnHeaderBuilding()
        {
        }

        public abstract void WriteHeader(ReportCell currentPosition, object v);


        public virtual void OnHeaderBuilt()
        {
        }

        public virtual void OnBodyBuilding()
        {
        }

        public abstract void WriteBodyCell(ReportCell currentPosition, object v, string format);


        public virtual void OnBodyBuilt()
        {
        }

        public virtual void OnFooterBuilding()
        {
        }

        public abstract void WriteFooterCell(ReportCell currentPosition, object v, string format);


        public virtual void OnFooterBuilt()
        {
        }

        public virtual void OnWrote()
        {
        }
    }
}