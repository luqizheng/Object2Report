using System;
using System.Collections;
using System.Collections.Generic;

namespace Coder.Object2Report
{
    public class Report
    {
        private int _rowIndex;

        public Report(IRender render)
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));
            Render = render;

            Columns = new List<IColumn>();
        }

        public IRender Render { get; set; }

        public IList<IColumn> Columns { get; }


        public void Write(IEnumerable data)
        {
            Render.OnWritting();
            WriteHeader();
            WriteBody(data);
            WriteFooter();
            Render.OnWrote();
        }


        public void WriteBody(IEnumerable data)
        {
            Render.OnBodyBuilding();
            foreach (var item in data)
            {
                var cellIndex = 0;
                foreach (var col in Columns)
                {
                    var currentPosition = GetCurrentCell(cellIndex);
                    var obj = col.GetValue(item);
                    Render.WriteBodyCell(currentPosition, obj, col.Format);
                    col.Footer?.Merge(obj);
                    cellIndex++;
                }
                _rowIndex++;
            }
            Render.OnBodyBuilt();
        }

        public void WriteFooter()
        {
            var cellIndex = 0;

            foreach (var col in Columns)
            {
                var value = col.Footer != null ? col.Footer.GetValue() : "";
                var currentPosition = GetCurrentCell(cellIndex);
                Render.WriteFooterCell(currentPosition, value, col.Format);
                cellIndex++;
            }
            _rowIndex++;
        }

        public void WriteHeader()
        {
            Render.OnHeaderBuilding();
            var cellIndex = 0;
            foreach (var col in Columns)
            {
                object title = col.Title;
                Render.WriteHeader(GetCurrentCell(cellIndex), title);

                cellIndex++;
            }
            _rowIndex++;
            Render.OnHeaderBuilt();
        }

        private ReportCell GetCurrentCell(int cellIndex)
        {
            return new ReportCell
            {
                Index = cellIndex,
                RowIndex = _rowIndex,
                MaxCell = Columns.Count
            };
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