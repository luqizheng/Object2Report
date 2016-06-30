using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IColumnFooterInfo<TResult>
    {
        FooterCell Footer { get; set; }
    }
}