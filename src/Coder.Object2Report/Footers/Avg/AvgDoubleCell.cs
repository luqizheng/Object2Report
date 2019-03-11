namespace Coder.Object2Report.Footers.Avg
{
    public class AvgDoubleCell : AvgBase<double>
    {
        protected override double Calculate(double currentResult, double mergeValue)
        {
            return currentResult + mergeValue;
        }

        protected override object GetAvgResult(int totalCount, double sumResult)
        {
            return sumResult/totalCount;
        }
    }
}