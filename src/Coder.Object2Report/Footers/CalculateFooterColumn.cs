using System;
using System.Linq.Expressions;

namespace Coder.Object2Report.Footers
{
    public abstract class CalculateFooterColumn<T, TResult> : FooterColumn<T>
        where T : new()
    {
        private readonly Func<T, TResult> _tFunc;
        protected TResult Result;

        protected CalculateFooterColumn(Expression<Func<T, TResult>> tFunc)
        {
            _tFunc = tFunc.Compile();
        }

        public override object GetValue(T t)
        {
            return Result;
        }

        public override void Merge(T c)
        {
            var f = _tFunc(c);
            Calculate(Result, f);
        }

        protected abstract TResult Calculate(TResult result, TResult mergeValue);
    }
}