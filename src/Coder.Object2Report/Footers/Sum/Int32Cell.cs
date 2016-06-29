namespace Coder.Object2Report.Footers.Sum
{
    public class Int32Cell : CalculateFooterCell<int>
    {
        protected override int Calculate(int result, int mergeValue)
        {
            return result + mergeValue;
        }
    }
}