namespace Coder.Object2Report.Footers.Sum
{
    public class Int64Cell : CalculateFooterCell<long>
    {
        protected override long Calculate(long result, long mergeValue)
        {
            return result + mergeValue;
        }
    }
}