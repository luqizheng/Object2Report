
using System;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using System.IO;

namespace Coder.Object2Report.Renders.NPOI
{
    /// <summary>
    ///     By NPOI HSSF Excel.
    /// </summary>
    public class HssfExcelRender : ExcelRender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="sheetName"></param>
        /// <param name="templateName"></param>
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
            if (TemplateExcelFile != null)
                using (var fileStream = File.OpenRead(TemplateExcelFile))
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
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (info == null) throw new ArgumentNullException(nameof(info));
            var workbook = (HSSFWorkbook)book;
            workbook.DocumentSummaryInformation = info.CreateDocumentInfo();
            workbook.SummaryInformation = info.CreateWorkBookInfo();
        }
    }
}
