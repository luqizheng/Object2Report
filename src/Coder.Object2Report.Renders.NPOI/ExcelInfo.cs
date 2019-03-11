using NPOI.HPSF;

namespace Coder.Object2Report.Renders.NPOI
{
    /// <summary>
    /// </summary>
    public class ExcelInfo
    {
        /// <summary>
        ///     Excel
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Subject { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Author { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Comment { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        internal DocumentSummaryInformation CreateDocumentInfo()
        {
            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = Company ?? "";
            return dsi;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
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