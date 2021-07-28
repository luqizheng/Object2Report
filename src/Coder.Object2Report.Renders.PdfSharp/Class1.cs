using System;

namespace Coder.Object2Report.Renders.PdfSharp
{
    public class PDFRender : RenderBase
    {
        public override void WriteHeader<TObject>(CellCursor<TObject> cellCursor, string title, string format)
        {
            throw new NotImplementedException();
        }

        public override void WriteBodyCell<T, TObject>(CellCursor<TObject> currentPosition, T v, string format)
        {
            throw new NotImplementedException();
        }

        public override void WriteFooterCell<T, TObject>(CellCursor<TObject> currentPosition, T v, string format)
        {
            throw new NotImplementedException();
        }
    }
}