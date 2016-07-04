using System;
using System.IO;
using System.Text;

namespace Coder.Object2Report.Renders
{
    /// <summary>
    ///     Reference https://help.github.com/articles/organizing-information-with-tables/
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

        public override void OnRowWritting(ReportCell cell, int rowIndex)
        {
            _curRows = new string[cell.MaxCell];
        }

        private string GetFormatPatten(string format)
        {
            if (format == null)
                return null;
            if (format.Contains("{"))
            {
                return format;
            }
            return "{0:" + format + "}";
        }

        public override void WriteBodyCell<T>(ReportCell currentPosition, T v, string format)
        {
            Write(currentPosition, v, format);
        }

        public override void WriteFooterCell<T>(ReportCell currentPosition, T v, string format)
        {
            Write(currentPosition, v, format);
        }

        public override void WriteHeader(ReportCell currentPosition, string title, string format)
        {
            Write(currentPosition, title, format);
        }

        public void Write<T>(ReportCell cell, T v, string format)
        {
            if (v == null)
                return;

            var value = string.IsNullOrEmpty(format) ? v.ToString() : string.Format(GetFormatPatten(format), v);

            if (v.ToString().IndexOf("|", StringComparison.Ordinal) != -1)
            {
                value = $"{value.Replace("|", "\"|")}";
            }
            _curRows[cell.Index] = value;
        }

        public override void OnRowWorte()
        {
            _writer.WriteLine("|" + string.Join("|", _curRows) + "|");
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