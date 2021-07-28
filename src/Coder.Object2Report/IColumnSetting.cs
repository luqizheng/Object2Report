using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public interface IColumnSetting<TResult>
    {
        FooterCell<TResult> Footer { get; set; }

        string Format { get; set; }

    }
}