namespace Coder.Object2Report.Footers
{
    public abstract class CalculateFooterCell<TResult> : FooterCell<TResult>
    {
        public override void Calculate(TResult t)
        {
            CellValue = Calculate(CellValue, t);
        }

        /// <summary>
        /// </summary>
        /// <param name="currentResult"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        protected abstract TResult Calculate(TResult currentResult, TResult newValue);
    }
}