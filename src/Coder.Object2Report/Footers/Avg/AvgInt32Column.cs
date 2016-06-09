namespace Coder.Object2Report.Footers.Avg
{
    public class AvgInt32Column : AvgBase<int>
    {
        protected override int Calculate(int result, int mergeValue)
        {
            return result + mergeValue;
        }
    }
}