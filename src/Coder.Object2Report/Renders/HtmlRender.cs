using System;
using System.IO;

namespace Coder.Object2Report.Renders
{
    public class HtmlRender : RenderBase
    {
        private readonly StreamWriter _writer;

        public HtmlRender(StreamWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            _writer = writer;
        }

        public string TableClass { get; set; }

        public override void OnReportWriting()
        {
            if (string.IsNullOrEmpty(TableClass))
                _writer.Write("<table>");
            else
                _writer.Write("<table class=\"{0}\">", TableClass);
        }

        public override void OnHeaderWriting()
        {
            _writer.Write("<thead>");
        }

        public override void OnHeaderWrote()
        {
            _writer.Write("</thead>");
        }

        public override void WriteHeader(CellCursor cellCursor, string title, string format)
        {
            Write("th", cellCursor, title);
        }

        public override void WriteBodyCell<T>(CellCursor currentPosition, T v, string format)
        {
            var value = string.Format(GetFormatPatten(format), v);
            Write("td", currentPosition, value);
        }

        public override void OnBodyBuilding()
        {
            _writer.Write("<tbody>");
        }

        public override void OnFooterWrote()
        {
            _writer.Write("</tbody>");
        }

        public override void WriteFooterCell<T>(CellCursor currentPosition, T v, string format)
        {
            var value = string.Format(GetFormatPatten(format), v);
            Write("td", currentPosition, value);
        }

        public override void OnReportWrote()
        {
            _writer.Write("</table>");
        }

        protected virtual void Write(string tag, CellCursor currentPosition, string v)
        {
            if (currentPosition.Index == 0)
                _writer.Write("<tr>");
            _writer.Write("<{1}>{0}</{1}>", v, tag);
            if (currentPosition.Index == currentPosition.MaxCell - 1)
            {
                _writer.Write("</tr>");
            }
        }

        private string GetFormatPatten(string format)
        {
            if (string.IsNullOrEmpty(format))
                return "{0}";
            if (format.Contains("{"))
            {
                return format;
            }
            return "{0:" + format + "}";
        }
    }
}