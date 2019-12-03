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
}
