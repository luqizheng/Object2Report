﻿namespace Coder.Object2Report.Footers.Sum
{
    public class Int64Column : CalculateFooterColumn<long>
    {
        protected override long Calculate(long result, long mergeValue)
        {
            return result + mergeValue;
        }
    }
}