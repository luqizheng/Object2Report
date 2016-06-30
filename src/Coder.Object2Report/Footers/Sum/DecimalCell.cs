namespace Coder.Object2Report.Footers.Sum
{
    public class DecimalCell : CalculateFooterCell<decimal>
    {
        protected override decimal Calculate(decimal currentResult, decimal mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}