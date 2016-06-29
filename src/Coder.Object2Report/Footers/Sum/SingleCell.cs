using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class SingleCell : CalculateFooterCell<float>
    {
        
        protected override float Calculate(float result, float mergeValue)
        {
            return result + mergeValue;
        }
    }
}