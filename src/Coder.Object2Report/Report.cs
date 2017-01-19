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
        /// </summary>
        private IRender _render;

        /// <summary>
        /// </summary>
        /// <param name="render"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Report(IRender render) : this()
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));
            _render = render;
        }

        /// <summary>
        /// </summary>
        public Report()
        {
            Columns = new List<IColumn<T>>();
        }

        internal CellCursor CellCursor => _currentCellCursor ?? (_currentCellCursor = new CellCursor(Columns.Count));

        /// <summary>
        /// </summary>
        public IRender Render
        {
            get { return _render; }
            set
            {
                _render = value;
                CellCursor.RowIndex = 0;
            }
        }

        /// <summary>
        /// </summary>
        public IList<IColumn<T>> Columns { get; }


        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public void Write(IEnumerable<T> data)
        {
            Render.OnReportWriting();
            WriteHeader();
            WriteBody(data);
            WriteFooter();
            Render.OnReportWrote();
        }


        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public void WriteBody(IEnumerable<T> data)
        {
            Render.OnBodyBuilding();
            foreach (var item in data)
            {
                Render.OnRowWriting(CellCursor, CellCursor.RowIndex);
                foreach (var col in Columns)
                {
                    CellCursor.Index = col.Index;
                    col.Write(item, Render.WriteBodyCell, CellCursor);
                }
                Render.OnRowWrote();
                CellCursor.NextRow();
            }
            Render.OnBodyBuilt();
        }

        /// <summary>
        /// </summary>
        public void WriteFooter()
        {
            Render.OnFooterWriting();
            Render.OnRowWriting(CellCursor, CellCursor.RowIndex);

            foreach (var col in Columns)
            {
                CellCursor.Index = col.Index;
                col.WriteFooter(Render.WriteFooterCell, CellCursor);
            }

            Render.OnRowWrote();
            Render.OnFooterWrote();
            CellCursor.NextRow();
        }

        /// <summary>
        /// </summary>
        public void WriteHeader()
        {
            Render.OnHeaderWriting();
            Render.OnRowWriting(CellCursor, CellCursor.RowIndex);
            foreach (var col in Columns)
            {
                CellCursor.Index = col.Index;
                Render.WriteHeader(CellCursor, col.Title, col.Format);
            }
            Render.OnRowWrote();
            Render.OnHeaderWrote();
            CellCursor.NextRow();
        }
    }
}