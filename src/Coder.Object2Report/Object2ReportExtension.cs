using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Coder.Object2Report.Renders;

namespace Coder.Object2Report
{
    public static class Object2ReportExtension
    {
        public static Report<T> WriteToCSV<T>(this Report<T> report, IEnumerable<T> data, string file, Encoding encoding)
        {
            using (var stream = File.OpenWrite(file))
            {
                var reader = new CsvRender(stream, encoding);

                report.Write(data, reader);
                if (stream.CanWrite)
                    stream.Flush();
            }

            return report;
        }
        public static Report<T> WriteToHTML<T>(this Report<T> report, IEnumerable<T> data, string file)
        {
            using (var stream = new StreamWriter(file))
            {
                var reader = new HtmlRender(stream);

                report.Write(data, reader);

                stream.Flush();
            }

            return report;
        }
        public static Report<T> WriteToMarkDown<T>(this Report<T> report, IEnumerable<T> data, string file, Encoding encoding)
        {
            using (var stream = File.OpenWrite(file))
            {
                var reader = new MarkDownRender(stream, encoding);

                report.Write(data, reader);
                stream.Flush();
            }

            return report;
        }

    }

    public class RendeException : Exception
    {
        public RendeException(string message) : base(message)
        {

        }
    }
    public class ReportWriter<T>
    {
        private readonly Report<T> _report;
        private readonly IRender _render;
        private bool _wroteFooter = false;
        private bool _wroteHeader = false;

        public ReportWriter(Report<T> report, IRender render)
        {
            _report = report;
            _render = render;
        }

        public virtual void Write(IEnumerable<T> data)
        {
            if (_wroteHeader && _wroteFooter)
            {
                throw new RendeException("Writer had been wrote.");
            }
            if (!_wroteHeader)
            {
                _report.WriteHeader(_render);
                _wroteHeader = true;
            }

            _report.WriteBody(data, _render);
        }

        public virtual void EndWrite()
        {
            if (!_wroteFooter)
            {
                _report.WriteFooter(_render);
                _wroteFooter = true;
            }
        }

    }
}
