using System;

namespace Coder.Object2Report.Footers
{
    public abstract class FooterCell<TResult>
    {
        public string Format { get; set; }

        public TResult CellValue { get; set; }

        public virtual void Write(Action<ReportCell, object, string> action, ReportCell cell)
        {
            action(cell, CellValue, Format);
        }

        public abstract void Calculate(TResult t);
    }
}