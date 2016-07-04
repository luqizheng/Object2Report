using System;
using System.Collections.Generic;

namespace Coder.Object2Report
{
    public class Report<T>
    {
        private ReportCell _currentCell;

        private IRender _render;

        public Report(IRender render) : this()
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));
            _render = render;
        }

        public Report()
        {
            Columns = new List<IColumn<T>>();
        }

        internal ReportCell CurrentCell
        {
            get
            {
                if (_currentCell == null)
                {
                    _currentCell = new ReportCell(Columns.Count);
                }
                return _currentCell;
            }
        }

        public IRender Render
        {
            get { return _render; }
            set
            {
                _render = value;
                CurrentCell.RowIndex = 0;
            }
        }

        public IList<IColumn<T>> Columns { get; }


        public void Write(IEnumerable<T> data)
        {
            Render.OnReportWritting();
            WriteHeader();
            WriteBody(data);
            WriteFooter();
            Render.OnReportWrote();
        }


        public void WriteBody(IEnumerable<T> data)
        {
            Render.OnBodyBuilding();
            foreach (var item in data)
            {
                Render.OnRowWritting(CurrentCell, CurrentCell.RowIndex);
                foreach (var col in Columns)
                {
                    CurrentCell.Index = col.Index;
                    col.Write(item, Render.WriteBodyCell, CurrentCell);
                }
                Render.OnRowWorte();
                CurrentCell.NextRow();
            }
            Render.OnBodyBuilt();
        }

        public void WriteFooter()
        {
            Render.OnFooterWritting();
            Render.OnRowWritting(CurrentCell, CurrentCell.RowIndex);

            foreach (var col in Columns)
            {
                CurrentCell.Index = col.Index;
                col.WriteFooter(Render.WriteFooterCell, CurrentCell);
            }

            Render.OnRowWorte();
            Render.OnFooterWrote();
            CurrentCell.NextRow();
        }

        public void WriteHeader()
        {
            Render.OnHeaderWritting();
            Render.OnRowWritting(CurrentCell, CurrentCell.RowIndex);
            foreach (var col in Columns)
            {
                CurrentCell.Index = col.Index;
                Render.WriteHeader(CurrentCell, col.Title, col.Format);
            }
            Render.OnRowWorte();
            Render.OnHeaderWrote();
            CurrentCell.NextRow();
        }
    }
}