using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Coder.Object2Report.Renders
{
    /// <summary>
    /// Reference https://help.github.com/articles/organizing-information-with-tables/
    /// </summary>
    public class MarkDownRender : RenderBase
    {
        private readonly StreamWriter _writer;

        private string[] _curRows;
        public MarkDownRender(Stream stream)
            : this(stream, Encoding.UTF8)
        {

        }
        public MarkDownRender(Stream writer, Encoding encoding)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            _writer = new StreamWriter(writer, encoding);
        }

        public override void OnRowWritting(Report report, int rowIndex)
        {
            _curRows = new string[report.Columns.Count];
        }

        public override void WriteBodyCell(ReportCell currentPosition, object v, string format)
        {
            Write(currentPosition, v, null);
        }

        public override void WriteFooterCell(ReportCell currentPosition, object v, string format)
        {
            Write(currentPosition, v, null);
        }

        public override void WriteHeader(ReportCell currentPosition, object v)
        {
            Write(currentPosition, v, null);
        }

        public void Write(ReportCell cell, object v, string format)
        {
            if (v == null)
                return;
            var value = string.IsNullOrEmpty(format) ? v.ToString() : String.Format(format, v);

            if (v.ToString().IndexOf("|", StringComparison.Ordinal) != -1)
            {
                value = $"{value.Replace("|", "\"|")}";
            }
            _curRows[cell.Index] = value;
        }

        public override void OnRowWorte()
        {
            _writer.WriteLine("|" + String.Join("|", _curRows) + "|");
        }

        public override void OnHeaderWrote()
        {
            WriteSpreadTag();
        }

        public override void OnReportWrote()
        {
            _writer.Flush();
            _writer.Dispose();
        }

        private void WriteSpreadTag()
        {
            var spliter = new string[_curRows.Length];
            for (var i = 0; i < spliter.Length; i++)
            {
                spliter[i] = "-";
            }
            _writer.WriteLine("|" + string.Join("|", spliter) + "|");
        }
    }
}
