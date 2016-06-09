using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class SingleColumn : CalculateFooterColumn<float>
    {
        
        protected override float Calculate(float result, float mergeValue)
        {
            return result + mergeValue;
        }
    }
}