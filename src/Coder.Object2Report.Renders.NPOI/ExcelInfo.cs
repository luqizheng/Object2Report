using NPOI.HPSF;

namespace Coder.Object2Report.Renders.NPOI
{
    public class ExcelInfo
    {
        /// <summary>
        ///     Excel
        /// </summary>
        public string Company { get; set; }

        public string Subject { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }

        internal DocumentSummaryInformation CreateDocumentInfo()
        {
            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = Company ?? "";
            return dsi;
        }

        internal SummaryInformation CreateWorkBookInfo()
        {
            ////create a entry of SummaryInformation
            var si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = Subject ?? "";
            si.Title = Title ?? "";
            si.Author = Author ?? "";
            si.Comments = Comment ?? "";
            return si;
        }
    }
}