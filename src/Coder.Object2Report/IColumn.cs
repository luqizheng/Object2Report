using System;
using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public interface IColumn<in T>
        where T : new()
    {
        string Title { get; set; }
        int Index { get; }
        string Format { get; set; }

        FooterColumn Footer { get; }
        object GetValue(T t);

    }

    public interface IColumnResult<TResult>
    {
        FooterColumn Footer { get; set; }

    }
}