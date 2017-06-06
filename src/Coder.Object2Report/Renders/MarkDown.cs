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


        public MarkDownRender(Stream writer, Encoding encoding)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            _writer = new StreamWriter(writer, encoding);
        }
#if NET45
        public MarkDownRender(Stream writer) : this(writer, Encoding.Default)
        {
        }
#else
         public MarkDownRender(Stream writer) : this(writer, Encoding.UTF8)
        {
        }
#endif
        public override void OnRowWriting(CellCursor cellCursor, int rowIndex)
        {
            _curRows = new string[cellCursor.MaxCell];
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

        public override void WriteBodyCell<T>(CellCursor currentPosition, T v, string format)
        {
            Write(currentPosition, v, format);
        }

        public override void WriteFooterCell<T>(CellCursor currentPosition, T v, string format)
        {
            Write(currentPosition, v, format);
        }

        public override void WriteHeader(CellCursor cellCursor, string title, string format)
        {
            Write(cellCursor, title, format);
        }

        public void Write<T>(CellCursor cellCursor, T v, string format)
        {
            if (v == null)
                return;

            var value = string.IsNullOrEmpty(format) ? v.ToString() : string.Format(GetFormatPatten(format), v);

            if (v.ToString().IndexOf("|", StringComparison.Ordinal) != -1)
            {
                value = $"{value.Replace("|", "\"|")}";
            }
            _curRows[cellCursor.Index] = value;
        }

        public override void OnRowWrote()
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