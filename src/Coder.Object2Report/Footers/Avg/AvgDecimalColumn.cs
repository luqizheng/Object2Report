namespace Coder.Object2Report.Footers.Avg
{
    public class AvgDecimalColumn : AvgBase<decimal>
    {
        protected override decimal Calculate(decimal currentResult, decimal mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}