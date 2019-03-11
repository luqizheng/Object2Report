using System;

namespace Coder.Object2Report
{
    public interface IColumn<in T>
    {
        string Title { get; }
        int Index { get; }

        /// <summary>
        ///     Refer https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx
        ///     Refer https://msdn.microsoft.com/en-us/library/0c899ak8(v=vs.110).aspx
        /// </summary>
        string Format { get; set; }

        void Write(T t, Action<CellCursor, object, string> action, CellCursor cellCursor);
        void WriteFooter(Action<CellCursor, object, string> action, CellCursor cellCursor);
    }
}