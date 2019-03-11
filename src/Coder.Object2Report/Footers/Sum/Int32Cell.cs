namespace Coder.Object2Report.Footers.Sum
{
    public class Int32Cell : CalculateFooterCell<int>
    {
        protected override int Calculate(int currentResult, int mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}