namespace Coder.Object2Report
{
    public interface IColumn<in T>
        where T : new()
    {
        string Title { get; set; }
        object GetValue(T t);
        int Index { get; set; }
    }
}