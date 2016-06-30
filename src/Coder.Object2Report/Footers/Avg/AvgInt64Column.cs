namespace Coder.Object2Report.Footers.Avg
{
    public class AvgInt64Column : AvgBase<long>
    {
        protected override long Calculate(long currentResult, long mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}