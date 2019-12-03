using System.Collections.Generic;
using System.IO;
using Coder.Object2Report.Renders.NPOI;

namespace Coder.Object2Report
{
    public static class ExcelObject2ReportExtensions
    {
        public static Report<T> WriteToXlsx<T>(this Report<T> report, IEnumerable<T> data, string file,
            string sheetName = "sheet1", string templateFilePath = null)
        {
            using (var stream = File.OpenWrite(file))
            {
                var reader = new XssfExcelReader(stream, sheetName, templateFilePath);

                report.Write(data, reader);
                stream.Flush();
            }

            return report;
        }

        public static Report<T> WriteToXls<T>(this Report<T> report, IEnumerable<T> data, string file,
            string sheetName = "sheet1", string templateFilePath = null)
        {
            using (var stream = File.OpenWrite(file))
            {
                var reader = new HssfExcelRender(stream, sheetName, templateFilePath);

                report.Write(data, reader);
                stream.Flush();
            }

            return report;
        }
    }
}