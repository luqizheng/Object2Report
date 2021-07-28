using System;

namespace Coder.Object2Report
{
    public class ColumnIndex<T> : Column<T, int>
    {
        private int _rowIndex = 1;

        public ColumnIndex(string title) : base(title, new Func<T, int>(t => 1))
        {
            Func = t => _rowIndex;
        }

        public override void Write(T t, Action<CellCursor<T>, object, string> action, CellCursor<T> cellCursor)
        {
            base.Write(t, action, cellCursor);
            _rowIndex++;
        }
    }
}