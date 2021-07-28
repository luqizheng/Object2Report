using System.IO;

namespace Coder.Object2Report
{
    public class ExcelWriter<T> : ReportWriter<T>
    {
        private readonly FileStream _stream;

        public ExcelWriter(Report<T> report, IRender render, FileStream stream) : base(report, render)
        {
            _stream = stream;
        }

        public override void EndWrite()
        {
            _stream.Close();
            base.EndWrite();
        }
    }
}