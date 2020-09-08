using System;

namespace Coder.Object2Report.Renders.PdfSharp
{
    public class PDFRender : RenderBase
    {
        public override void WriteHeader(CellCursor cellCursor, string title, string format)
        {
            throw new NotImplementedException();
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