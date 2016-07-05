using System;

namespace Coder.Object2Report.Footers
{
    public class FooterComment<TResult> : FooterCell<TResult>
    {
        private readonly string _cellValue;

        public FooterComment(string value)
        {
            _cellValue = value;
        }

        public override void Write(Action<ReportCell, object, string> action, ReportCell cell)
        {
            action(cell, _cellValue, Format);
        }

        public override void Calculate(TResult t)
        {
        }
    }
}