namespace Coder.Object2Report.Footers.Avg
{
    public class AvgInt32Cell : AvgBase<int>
    {
        protected override int Calculate(int currentResult, int mergeValue)
        {
            return currentResult + mergeValue;
        }

        protected override object GetAvgResult(int totalCount, int sumResult)
        {
            return sumResult/totalCount;
        }
    }
}