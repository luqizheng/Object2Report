using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public interface IColumn
    {
        string Title { get; }
        int Index { get; }
        string Format { get; set; }
        FooterCell Footer { get; }

        object GetValue(object item);
    }

    public interface IColumn<in T> : IColumn
        where T : new()
    {
        /// <summary>
        ///     get the output value from T Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        object GetValue(T model);
    }
}