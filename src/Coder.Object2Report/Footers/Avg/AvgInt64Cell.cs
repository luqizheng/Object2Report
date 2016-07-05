namespace Coder.Object2Report.Footers.Avg
{
    public class AvgInt64Cell : AvgBase<long>
    {
        protected override long Calculate(long currentResult, long mergeValue)
        {
            return currentResult + mergeValue;
        }

        protected override object GetAvgResult(int totalCount, long sumResult)
        {
            return sumResult / totalCount;
        }
    }
}