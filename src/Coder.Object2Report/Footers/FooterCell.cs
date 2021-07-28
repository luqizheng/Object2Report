using System;

namespace Coder.Object2Report.Footers
{
    public abstract class FooterCell<TResult>
    {
        public string Format { get; set; }

        public TResult CellValue { get; set; }

        public virtual void Write<TObject>(Action<CellCursor<TObject>, object, string> action, CellCursor<TObject> cellCursor)
        {
            action(cellCursor, CellValue, Format);
        }

        public abstract void Calculate(TResult t);
    }
}