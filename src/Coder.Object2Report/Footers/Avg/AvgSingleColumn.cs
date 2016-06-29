namespace Coder.Object2Report.Footers.Avg
{
    public class AvgSingleColumn : AvgBase<float>
    {
        protected override float Calculate(float result, float mergeValue)
        {
            return result + mergeValue;
        }
    }
}