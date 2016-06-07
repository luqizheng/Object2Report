using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class Int64Column<T> : CalculateFooterColumn<T, long> where T : new()
    {
        public Int64Column(Expression<Func<T, long>> tFunc) : base(tFunc)
        {
        }


        protected override long Calculate(long result, long mergeValue)
        {
            return result + mergeValue;
        }
    }
}