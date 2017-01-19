namespace Coder.Object2Report
{
    public abstract class RenderBase : IRender
    {
        /// <summary>
        /// </summary>
        public virtual void OnReportWriting()
        {
        }

        /// <summary>
        /// </summary>
        public virtual void OnHeaderWriting()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="cellCursor"></param>
        /// <param name="title"></param>
        /// <param name="format"></param>
        public abstract void WriteHeader(CellCursor cellCursor, string title, string format);

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnHeaderWrote()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void OnBodyBuilding()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        public abstract void WriteBodyCell<T>(CellCursor currentPosition, T v, string format);

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnBodyBuilt()
        {
        }

        public virtual void OnFooterWriting()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        public abstract void WriteFooterCell<T>(CellCursor currentPosition, T v, string format);

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnFooterWrote()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void OnReportWrote()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void OnRowWrote()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellCursor"></param>
        /// <param name="rowIndex"></param>
        public virtual void OnRowWriting(CellCursor cellCursor, int rowIndex)
        {
        }
    }
}