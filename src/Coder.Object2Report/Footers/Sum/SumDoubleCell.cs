namespace Coder.Object2Report.Footers.Sum
{
    public class SumDoubleCell : CalculateFooterCell<double>
    {
        protected override double Calculate(double currentResult, double mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}