namespace Coder.Object2Report.Footers.Avg
{
    public class AvgSingleCell : AvgBase<float>
    {
        protected override float Calculate(float currentResult, float mergeValue)
        {
            return currentResult + mergeValue;
        }

        protected override object GetAvgResult(int totalCount, float sumResult)
        {
            return sumResult / totalCount;
        }
    }
}