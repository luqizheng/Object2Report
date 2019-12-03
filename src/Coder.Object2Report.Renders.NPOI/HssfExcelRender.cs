
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;

using System.IO;
using NPOI.POIFS.FileSystem;

namespace Coder.Object2Report.Renders.NPOI
{
    /// <summary>
    ///     By NPOI HSSF Excel.
    /// </summary>
    public class HssfExcelRender : ExcelRender
    {
        public HssfExcelRender(Stream stream, string sheetName = "sheet1", string templateName = null) : base(stream,
            sheetName,
            templateName)
        {
            HeaderStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            FooterStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected override IWorkbook CreateWorkBook()
        {
            if (TemplateExceFile != null)
                using (var fileStream = File.OpenRead(TemplateExceFile))
                {
                    return new HSSFWorkbook(new POIFSFileSystem(fileStream));
                }

            return new HSSFWorkbook();
        }

        /// <summary>
        /// </summary>
        /// <param name="book"></param>
        /// <param name="info"></param>
        protected override void InitWorkbookInfo(IWorkbook book, ExcelInfo info)
        {
            var workbook = (HSSFWorkbook) book;
            workbook.DocumentSummaryInformation = info.CreateDocumentInfo();
            workbook.SummaryInformation = info.CreateWorkBookInfo();
        }
    }
}