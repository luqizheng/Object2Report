#if NETSTANDARD1_6
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;
#else
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
#endif
using System.IO;

namespace Coder.Object2Report.Renders.NPOI
{
    public class XssfExcelReader : ExcelRender
    {
        private static readonly byte[] DefColor =
        {
            0xDC, 0xE0, 0xE2
        };

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        public XssfExcelReader(Stream stream) : base(stream)
        {
            InitDefaultColor();
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="worksheetName"></param>
        public XssfExcelReader(Stream stream, string worksheetName, string templateFile) : base(stream, worksheetName,
            templateFile)
        {
            InitDefaultColor();
        }

        private void InitDefaultColor()
        {
            ((XSSFCellStyle) HeaderStyle).FillForegroundXSSFColor
                = new XSSFColor(DefColor);
            ((XSSFCellStyle) FooterStyle).FillForegroundXSSFColor
                = new XSSFColor(DefColor);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected override IWorkbook CreateWorkBook()
        {
            if (!string.IsNullOrEmpty(TemplateExceFile)) return new XSSFWorkbook(TemplateExceFile);

            return new XSSFWorkbook();
        }

        protected override void InitWorkbookInfo(IWorkbook book, ExcelInfo info)
        {
            var workbook = (XSSFWorkbook) book;
            var xmlProps = workbook.GetProperties();
            xmlProps.CoreProperties.Creator = info.Author ?? "";
            xmlProps.CoreProperties.Subject = info.Subject ?? "";
            xmlProps.CoreProperties.Title = info.Title ?? "";
            xmlProps.CoreProperties.Description = info.Comment;
            xmlProps.CoreProperties.Category = info.Company;
        }
    }
}