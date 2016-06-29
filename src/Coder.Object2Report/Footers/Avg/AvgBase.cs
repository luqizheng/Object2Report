namespace Coder.Object2Report.Footers.Avg
{
    public abstract class AvgBase<T> : CalculateFooterCell<T>
    {
        private int _total;

        public override void Merge(object c)
        {
            _total++;
            base.Merge(c);
        }

        public override object GetValue()
        {
            var re = (float)base.GetValue();
            return re / _total;
        }
    }
}