using System;

namespace Coder.Object2Report.Footers
{
    public class FooterComment<TResult> : FooterCell<TResult>
    {
        private readonly string _cellValue;

        public FooterComment(string value)
        {
            _cellValue = value;
        }

        //public override void Write(Action<CellCursor, object, string> action, CellCursor cellCursor)
        //{
        //    action(cellCursor, _cellValue, Format);
        //}

        public override void Write<TObject>(Action<CellCursor<TObject>, object, string> action, CellCursor<TObject> cellCursor)
        {
            action(cellCursor, _cellValue, Format);
        }

        public override void Calculate(TResult t)
        {
        }
    }
}