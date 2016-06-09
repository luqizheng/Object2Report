namespace Coder.Object2Report.Footers.Avg
{
    public class AvgDoubleColumn : AvgBase<double>
    {
        protected override double Calculate(double result, double mergeValue)
        {
            return result + mergeValue;
        }
    }
}