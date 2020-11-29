using System.Collections.Generic;

namespace Coder.Object2Report
{
    public class ReportWriter<T>
    {
        private readonly IRender _render;
        private readonly Report<T> _report;
        private bool _wroteFooter;
        private bool _wroteHeader;

        public ReportWriter(Report<T> report, IRender render)
        {
            _report = report;
            _render = render;
        }

        public virtual void Write(IEnumerable<T> data)
        {
            if (_wroteHeader && _wroteFooter) throw new RenderException("Writer had been wrote.");
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

            _render.OnReportWrote();
        }
    }
}
