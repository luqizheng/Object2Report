using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers.Sum
{
    public class Int32Column<T> : CalculateFooterColumn<T, Int32> where T : new()
    {
        public Int32Column(Expression<Func<T, Int32>> tFunc) : base(tFunc)
        {
        }


        protected override Int32 Calculate(Int32 result, Int32 mergeValue)
        {
            return result + mergeValue;
        }
    }
}