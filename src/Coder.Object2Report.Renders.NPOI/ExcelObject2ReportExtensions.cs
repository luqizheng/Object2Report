using Coder.Object2Report.Renders.NPOI;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.IO;

// ReSharper disable once CheckNamespace
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
                if (stream.CanWrite)
                    stream.Flush();
            }

            return report;
        }
        /// <summary>
        /// 一次性写入xls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="report"></param>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <param name="sheetName"></param>
        /// <param name="templateFilePath"></param>
        /// <returns></returns>
        public static Report<T> WriteToXls<T>(this Report<T> report, IEnumerable<T> data, string file,
            string sheetName = "sheet1", string templateFilePath = null)
        {
            using (var stream = File.OpenWrite(file))
            {
                var reader = new HssfExcelRender(stream, sheetName, templateFilePath);

                report.Write(data, reader);
                if (stream.CanWrite)
                    stream.Flush();
            }

            return report;
        }
        /// <summary>
        /// 用于多次写入
        /// </summary>
        /// <param name="report"></param>
        /// <param name="file"></param>
        /// <param name="sheetName"></param>
        /// <param name="templateFilePath"></param>
        /// <returns></returns>
        public static ExcelWriter<T> GetXlsxWriter(this Report<T> report, string file, string sheetName = "sheet1",
            string templateFilePath = null)
        {
            var stream = File.OpenWrite(file);
            var reader = new XssfExcelReader(stream, sheetName, templateFilePath);
            var result = new ExcelWriter<T>(report, reader, stream);
            return result;
        }
        /// <summary>
        /// 用于多次写入
        /// </summary>
        /// <param name="report"></param>
        /// <param name="file"></param>
        /// <param name="sheetName"></param>
        /// <param name="templateFilePath"></param>
        /// <returns></returns>
        public static ExcelWriter<T> GetXlsWriter(this Report<T> report, string file, string sheetName = "sheet1",
            string templateFilePath = null)
        {
            var stream = File.OpenWrite(file);
            var reader = new HssfExcelRender(stream, sheetName, templateFilePath);
            var result = new ExcelWriter<T>(report, reader, stream);
            return result;
        }
    }


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
