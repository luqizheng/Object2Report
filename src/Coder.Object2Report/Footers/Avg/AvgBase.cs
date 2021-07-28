using System;

namespace Coder.Object2Report.Footers.Avg
{
    public abstract class AvgBase<T> : CalculateFooterCell<T>
    {
        private int _total;

        public override void Calculate(T c)
        {
            _total++;
            base.Calculate(c);
        }

        protected abstract object GetAvgResult(int totalCount, T sumResult);

        public override void Write<TObject>(Action<CellCursor<TObject>, object, string> action, CellCursor<TObject> cellCursor)
        {
            var v = GetAvgResult(_total, CellValue);
            action(cellCursor, v, Format);
        }
    }
}