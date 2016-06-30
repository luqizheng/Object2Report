namespace Coder.Object2Report.Footers.Sum
{
    public class SingleCell : CalculateFooterCell<float>
    {
        protected override float Calculate(float currentResult, float mergeValue)
        {
            return currentResult + mergeValue;
        }
    }
}