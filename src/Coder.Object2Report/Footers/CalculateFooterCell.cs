namespace Coder.Object2Report.Footers
{
    public abstract class CalculateFooterCell<T> : FooterCell

    {
        /// <summary>
        /// 
        /// </summary>
        protected T Result;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public override void Merge(object c)
        {
            Result = Calculate(Result, (T) c);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentResult"></param>
        /// <param name="mergeValue"></param>
        /// <returns></returns>
        protected abstract T Calculate(T currentResult, T mergeValue);
    }
}