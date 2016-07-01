using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Coder.Object2Report.Renders.Excel
{
    public class XssfExcelReader : ExcelRender
    {
        private static readonly byte[] DefColor =
        {
            0xDC, 0xE0, 0xE2
        };

        public XssfExcelReader(Stream stream) : base(stream)
        {
            InitDefaultColor();
        }

        public XssfExcelReader(Stream stream, string worksheetName) : base(stream, worksheetName)
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

        protected override IWorkbook CreateWorkBook()
        {
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