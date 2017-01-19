using System;
using Pomelo.Data.Excel;
using Pomelo.Data.Excel.Infrastructure;

namespace Coder.Object2Report.Renders.PomeloExcel
{
    public class ExcelRender : RenderBase
    {
        private readonly string _excelpath;
        private readonly ExcelStream _excelStream;

        /// <summary>
        /// </summary>
        /// <param name="excelpath"></param>
        public ExcelRender(string excelpath)
        {
            if (excelpath == null) throw new ArgumentNullException(nameof(excelpath));
            _excelpath = excelpath;
            _excelStream = new ExcelStream();
        }

        /// <summary>
        /// </summary>
        /// <param name="cellCursor"></param>
        /// <param name="title"></param>
        /// <param name="format"></param>
        public override void WriteHeader(CellCursor cellCursor, string title, string format)
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

        public override void WriteBodyCell<T>(CellCursor currentPosition, T v, string format)
        {
            throw new NotImplementedException();
        }

        public override void WriteFooterCell<T>(CellCursor currentPosition, T v, string format)
        {
            throw new NotImplementedException();
        }
    }
}