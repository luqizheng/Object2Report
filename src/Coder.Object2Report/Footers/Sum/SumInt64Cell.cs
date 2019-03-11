namespace Coder.Object2Report.Footers.Sum
{
    public class SumInt64Cell : CalculateFooterCell<long>
    {
        protected override long Calculate(long currentResult, long mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}