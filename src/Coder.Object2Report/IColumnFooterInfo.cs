using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public interface IColumnFooterInfo<TResult>
    {
        FooterCell Footer { get; set; }
    }
}