using System;
using System.Collections.Generic;

namespace Coder.Object2Report
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Report<T>
    {
        /// <summary>
        /// </summary>
        private CellCursor _currentCellCursor;

    
        /// <summary>
        /// 
        /// </summary>
        public Report()
        {
            Columns = new List<IColumn<T>>();
        }

        public int StartRowIndex { get; set; } = 0;
        public bool RenderTitle { get; set; } = true;
        internal CellCursor CellCursor => _currentCellCursor ?? (_currentCellCursor = new CellCursor(Columns.Count));



        /// <summary>
        /// </summary>
        public IList<IColumn<T>> Columns { get; }


        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public void Write(IEnumerable<T> data, IRender render)
        {
            if (render == null) throw new ArgumentNullException(nameof(render));

            CellCursor.RowIndex = StartRowIndex;
            render.OnReportWriting();
            if (RenderTitle)
                WriteHeader(render);
            WriteBody(data,render);
            WriteFooter(render);
            render.OnReportWrote();
        }


        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public void WriteBody(IEnumerable<T> data, IRender render)
        {
            if (render == null) throw new ArgumentNullException(nameof(render));

            render.OnBodyBuilding();
            foreach (var item in data)
            {
                render.OnRowWriting(CellCursor, CellCursor.RowIndex);
                foreach (var col in Columns)
                {
                    CellCursor.Index = col.Index;
                    col.Write(item, render.WriteBodyCell, CellCursor);
                }

                render.OnRowWrote();
                CellCursor.NextRow();
            }

            render.OnBodyBuilt();
        }

        /// <summary>
        /// </summary>
        public void WriteFooter(IRender render)
        {
            if (render == null) throw new Object2ReportException("Render is not set.");
            render.OnFooterWriting();
            render.OnRowWriting(CellCursor, CellCursor.RowIndex);

            foreach (var col in Columns)
            {
                CellCursor.Index = col.Index;
                col.WriteFooter(render.WriteFooterCell, CellCursor);
            }

            render.OnRowWrote();
            render.OnFooterWrote();
            CellCursor.NextRow();
        }

        /// <summary>
        /// </summary>
        public void WriteHeader(IRender render)
        {
            if (render == null) throw new Object2ReportException("Render is not set.");
            render.OnHeaderWriting();
            render.OnRowWriting(CellCursor, CellCursor.RowIndex);
            foreach (var col in Columns)
            {
                CellCursor.Index = col.Index;
                render.WriteHeader(CellCursor, col.Title, col.Format);
            }

            render.OnRowWrote();
            render.OnHeaderWrote();
            CellCursor.NextRow();
        }
    }
}