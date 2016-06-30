namespace Coder.Object2Report.Footers.Avg
{
    public class AvgInt32Column : AvgBase<int>
    {
        protected override int Calculate(int currentResult, int mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}