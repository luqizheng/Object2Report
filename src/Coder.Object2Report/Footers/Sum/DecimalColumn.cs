﻿namespace Coder.Object2Report.Footers.Sum
{
    public class DecimalColumn : CalculateFooterColumn<decimal>
    {
        protected override decimal Calculate(decimal result, decimal mergeValue)
        {
            return result + mergeValue;
        }
    }
}