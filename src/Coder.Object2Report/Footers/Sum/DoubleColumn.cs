using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class DoubleColumn : CalculateFooterColumn< double> 
    {
        
        protected override Double Calculate(Double result, Double mergeValue)
        {
            return result + mergeValue;
        }
    }
}