namespace Coder.Object2Report.Footers.Sum
{
    public class DoubleCell : CalculateFooterCell<double>
    {
        protected override double Calculate(double result, double mergeValue)
        {
            return result + mergeValue;
        }
    }
}