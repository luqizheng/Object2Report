namespace Coder.Object2Report.Footers.Avg
{
    public class AvgInt64Column : AvgBase<long>
    {
        protected override long Calculate(long result, long mergeValue)
        {
            return result + mergeValue;
        }
    }
}