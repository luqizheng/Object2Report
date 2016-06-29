using System;
using System.IO;

namespace Coder.Object2Report.Renders
{
    public class HtmlRender : RenderBase
    {
        private readonly StreamWriter _writer;

        public HtmlRender(StreamWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            _writer = writer;
        }

        public override void OnWritting()
        {
            _writer.Write("<table>");
        }

        public override void OnHeaderBuilding()
        {
            _writer.Write("<thead>");
        }

        public override void OnHeaderBuilt()
        {
            _writer.Write("</thead>");
        }

        public override void WriteHeader(ReportCell currentPosition, object v)
        {
            Write("th", currentPosition, v);
        }

        public override void WriteBodyCell(ReportCell currentPosition, object v, string format)
        {
            Write("td", currentPosition, v);
        }

        public override void WriteFooterCell(ReportCell currentPosition, object v, string format)
        {
            Write("td", currentPosition, v);
        }

        public override void OnWrote()
        {
            _writer.Write("</table>");
        }

        protected virtual void Write(string tag, ReportCell currentPosition, object v)
        {
            if (currentPosition.Index == 0)
                _writer.Write("<tr>");
            _writer.Write(string.Format("<{1}>{0}</{1}>", v, tag));
            if (currentPosition.Index == currentPosition.MaxCell - 1)
            {
                _writer.Write("</tr>");
            }
        }
    }
}