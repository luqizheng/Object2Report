using System.IO;

#if NETSTANDARD1_6
using Npoi.Core.SS.UserModel;
using Npoi.Core.HSSF.UserModel;
using Npoi.Core.HSSF.Util;

#else
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
#endif

namespace Coder.Object2Report.Renders.NPOI
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