using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace Coder.Object2Report.Renders.Excel
{
    /// <summary>
    ///     By NPOI HSSF Excel.
    /// </summary>
    public class HssfExcelRender : ExcelRender
    {
        public HssfExcelRender(Stream stream, string sheetName = "sheet1") : base(stream, sheetName)
        {
            HeaderStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            FooterStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
        }

        protected override IWorkbook CreateWorkBook()
        {
            return new HSSFWorkbook();
        }

        protected override void InitWorkbookInfo(IWorkbook book, ExcelInfo info)
        {
            var workbook = (HSSFWorkbook) book;
            workbook.DocumentSummaryInformation = info.CreateDocumentInfo();
            workbook.SummaryInformation = info.CreateWorkBookInfo();
        }
    }
}