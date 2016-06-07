using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class SingleColumn<T> : CalculateFooterColumn<T, float> where T : new()
    {
        public SingleColumn(Expression<Func<T, float>> tFunc) : base(tFunc)
        {
        }


        protected override float Calculate(float result, float mergeValue)
        {
            return result + mergeValue;
        }
    }
}