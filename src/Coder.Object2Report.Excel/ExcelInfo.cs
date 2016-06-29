using NPOI.HPSF;

namespace Coder.Object2Report.Renders.Excel
{
    public class ExcelInfo
    {
        public string Company { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }

        public DocumentSummaryInformation CreateDocumentInfo()
        {
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = this.Company ?? "";
            return dsi;


        }

        public SummaryInformation CreateWorkBookInfo()
        {
            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = this.Subject ?? "";
            si.Title = this.Title ?? "";
            si.Author = this.Author ?? "";
            si.Comments = this.Comment ?? "";
            return si;
        }
    }
}