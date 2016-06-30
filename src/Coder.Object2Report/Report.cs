using System;
using System.Collections;
using System.Collections.Generic;

namespace Coder.Object2Report
{
    public class Report
    {
        private readonly ReportCell _currentCell;

        private IRender _render;

        public Report(IRender render)
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));
            Columns = new List<IColumn>();
            _currentCell = new ReportCell(this);
            Render = render;
        }

        public IRender Render
        {
            get { return _render; }
            set
            {
                _render = value;
                _currentCell.RowIndex = 0;
            }
        }

        public IList<IColumn> Columns { get; }


        public void Write(IEnumerable data)
        {
            Render.OnReportWritting();
            WriteHeader();
            WriteBody(data);
            WriteFooter();
            Render.OnReportWrote();
        }


        public void WriteBody(IEnumerable data)
        {
            Render.OnBodyBuilding();
            foreach (var item in data)
            {
                Render.OnRowWritting(this, _currentCell.RowIndex);
                foreach (var col in Columns)
                {
                    _currentCell.SetCell(col);
                    var value = col.GetValue(item);
                    Render.WriteBodyCell(_currentCell, value, col.Format);
                    col.Footer?.Merge(value);
                }
                Render.OnRowWorte();
                _currentCell.NextRow();
            }
            Render.OnBodyBuilt();
        }

        public void WriteFooter()
        {
            Render.OnFooterWritting();
            Render.OnRowWritting(this, _currentCell.RowIndex);
            foreach (var col in Columns)
            {
                if (col.Footer == null)
                    continue;
                var value = col.Footer.GetValue() ?? "";
                _currentCell.SetCell(col);
                Render.WriteFooterCell(_currentCell, value, col.Format);
            }
            Render.OnRowWorte();
            Render.OnFooterWrote();
            _currentCell.NextRow();
        }

        public void WriteHeader()
        {
            Render.OnHeaderWritting();
            Render.OnRowWritting(this, _currentCell.RowIndex);
            foreach (var col in Columns)
            {
                object title = col.Title;
                _currentCell.SetCell(col);
                Render.WriteHeader(_currentCell, title);
            }
            Render.OnRowWorte();
            Render.OnHeaderWrote();
            _currentCell.NextRow();
        }
    }

    public class Report<T> : Report
    {
        public Report(IRender render) : base(render)
        {
        }

        public void Write(IEnumerable<T> data)
        {
            base.Write(data);
        }

        public void WriteBody(IEnumerable<T> data)
        {
            base.WriteBody(data);
        }
    }
}