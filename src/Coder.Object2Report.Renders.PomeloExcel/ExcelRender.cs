using System;
using Pomelo.Data.Excel;
using Pomelo.Data.Excel.Infrastructure;

namespace Coder.Object2Report.Renders.PomeloExcel
{
    public class ExcelRender : RenderBase
    {
        private readonly string _excelpath;
        private readonly ExcelStream _excelStream;

        public ExcelRender(string excelpath)
        {
            _excelpath = excelpath;
            _excelStream = new ExcelStream();
        }

        public override void WriteHeader(ReportCell currentPosition, string title, string format)
        {
            using (var x = _excelStream.Create(_excelpath))
            {
                using (var sheet = x.LoadSheet(1))
                {
                    sheet.Add(new Row
                    {
                        "Create test"
                    });
                    sheet.SaveChanges();
                }
            }
        }

        public override void WriteBodyCell<T>(ReportCell currentPosition, T v, string format)
        {
            throw new NotImplementedException();
        }

        public override void WriteFooterCell<T>(ReportCell currentPosition, T v, string format)
        {
            throw new NotImplementedException();
        }
    }
}