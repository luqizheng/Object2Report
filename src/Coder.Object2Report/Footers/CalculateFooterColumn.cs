namespace Coder.Object2Report.Footers
{
    public abstract class CalculateFooterColumn<T> : FooterColumn

    {
        protected T Result;


        public override object GetValue()
        {
            return Result;
        }

        public override void Merge(object c)
        {
            Calculate(Result, (T)c);
        }

        protected abstract T Calculate(T result, T mergeValue);
    }
    
}