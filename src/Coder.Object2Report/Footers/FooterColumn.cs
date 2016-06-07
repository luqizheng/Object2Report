namespace Coder.Object2Report.Footers
{
    public abstract class FooterColumn<T> : IColumn<T>
        where T : new()
    {
        public virtual string Title { get; set; }


        public virtual object GetValue(T t)
        {
            return Title;
        }

        public int Index { get; set; }

        public abstract void Merge(T c);
    }
}