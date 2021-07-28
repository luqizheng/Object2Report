using System;

namespace Coder.Object2Report
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="column"></param>
    /// <param name="rowIndex"></param>
    /// <param name="cellIndex"></param>
    /// <param name="cell"></param>
    public delegate void BuiltEvent<TObject>(IColumn<TObject> column, int rowIndex, int cellIndex, object cell);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    /// <param name="column"></param>
    /// <param name="rowIndex"></param>
    /// <param name="cellIndex"></param>
    /// <param name="cell"></param>
    public delegate void BuiltEvent<TObject,TCell>(IColumn<TObject> column, int rowIndex, int cellIndex, TCell cell);
    public interface IColumn<T>
    {
        /// <summary>
        /// 
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 
        /// </summary>
        int Index { get; }

        /// <summary>
        ///     Refer https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx
        ///     Refer https://msdn.microsoft.com/en-us/library/0c899ak8(v=vs.110).aspx
        /// </summary>
        string Format { get; set; }

        BuiltEvent<T> OnBuiltCell { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="action"></param>
        /// <param name="cellCursor"></param>

        void Write(T t, Action<CellCursor<T>, object, string> action, CellCursor<T> cellCursor);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cellCursor"></param>
        void WriteFooter(Action<CellCursor<T>, object, string> action, CellCursor<T> cellCursor);
    }
}