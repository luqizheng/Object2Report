﻿using System;
using System.Collections.Generic;
using Coder.Object2Report.Footers;
using Coder.Object2Report.Footers.Sum;

namespace Coder.Object2Report
{
    public static class FooterExtend
    {
        private static readonly Dictionary<Type, Func<FooterCell>> SumFactory =
            new Dictionary<Type, Func<FooterCell>>
            {
                {typeof(decimal), () => new DecimalCell()},
                {typeof(int), () => new DecimalCell()},
                {typeof(long), () => new DecimalCell()},
                {typeof(float), () => new DecimalCell()},
                {typeof(double), () => new DecimalCell()}
            };

        private static readonly Dictionary<Type, Func<FooterCell>> AvgFactory =
            new Dictionary<Type, Func<FooterCell>>
            {
                {typeof(decimal), () => new DecimalCell()},
                {typeof(int), () => new DecimalCell()},
                {typeof(long), () => new DecimalCell()},
                {typeof(float), () => new DecimalCell()},
                {typeof(double), () => new DecimalCell()}
            };


        public static FooterCell Sum<T>(this IColumnFooterInfo<T> column)
            where T : new()
        {
            var tType = typeof(T);
            if (!SumFactory.ContainsKey(tType))
            {
                throw new ArgumentOutOfRangeException("column", "Do not support type of " + tType.Name);
            }

            var result = SumFactory[tType]();
            column.Footer = result;
            return result;
        }

        public static FooterCell Avg<T>(this IColumnFooterInfo<T> column)
            where T : new()
        {
            var tType=typeof(T);
            if(!AvgFactory.ContainsKey(tType))
            { 
                throw new ArgumentOutOfRangeException("column","Do not support type of "+tType.Name);

            }
            var result = AvgFactory[tType]();
            column.Footer = result;
            return result;
        }

        public static FooterCell Comment<T>(this IColumnFooterInfo<T> column, string footerName)
        {
            if (footerName == null)
                throw new ArgumentNullException(nameof(footerName));
            column.Footer = new FooterComment(footerName);
            return column.Footer;
        }
    }
}