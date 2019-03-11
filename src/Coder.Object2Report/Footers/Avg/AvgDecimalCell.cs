namespace Coder.Object2Report.Footers.Avg
{
    public class AvgDecimalCell : AvgBase<decimal>
    {
        protected override decimal Calculate(decimal currentResult, decimal mergeValue)
        {
            return currentResult + mergeValue;
        }

        protected override object GetAvgResult(int totalCount, decimal sumResult)
        {
            return sumResult/totalCount;
        }
    }
}