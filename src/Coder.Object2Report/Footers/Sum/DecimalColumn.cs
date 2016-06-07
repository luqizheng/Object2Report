using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class DecimalColumn<T> : CalculateFooterColumn<T, decimal> where T : new()
    {
        public DecimalColumn(Expression<Func<T, decimal>> tFunc) : base(tFunc)
        {
        }

        protected override decimal Calculate(decimal result, decimal mergeValue)
        {
            return result + mergeValue;
        }
    }
}