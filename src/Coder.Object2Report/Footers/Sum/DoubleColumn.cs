using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class DoubleColumn<T> : CalculateFooterColumn<T, double> where T : new()
    {
        public DoubleColumn(Expression<Func<T, double>> tFunc) : base(tFunc)
        {
        }


        protected override Double Calculate(Double result, Double mergeValue)
        {
            return result + mergeValue;
        }
    }
}