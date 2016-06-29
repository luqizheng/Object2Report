namespace Coder.Object2Report.Footers.Sum
{
    public class DecimalCell : CalculateFooterCell<decimal>
    {
        protected override decimal Calculate(decimal result, decimal mergeValue)
        {
            return result + mergeValue;
        }
    }
}