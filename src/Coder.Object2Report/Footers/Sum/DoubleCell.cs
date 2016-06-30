namespace Coder.Object2Report.Footers.Sum
{
    public class DoubleCell : CalculateFooterCell<double>
    {
        protected override double Calculate(double currentResult, double mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}