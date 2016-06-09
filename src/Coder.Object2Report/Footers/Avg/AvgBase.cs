namespace Coder.Object2Report.Footers.Avg
{
    public abstract class AvgBase<T> : CalculateFooterColumn<T>
    {
        private int _total;

        public override void Merge(object c)
        {
            _total++;
            base.Merge(c);
        }

        public override object GetValue()
        {
            var re = (float)GetValue();
            return re / _total;
        }
    }
}