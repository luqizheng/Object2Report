using System;
using Coder.Object2Report.Footers;
using Coder.Object2Report.Footers.Avg;
using Coder.Object2Report.Footers.Sum;

namespace Coder.Object2Report
{
    public static class FooterExtend
    {
        /// <summary>
        ///     Value of Footer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="footerName"></param>
        /// <returns></returns>
        public static FooterComment<T> Content<T>(this IColumnSetting<T> column, string footerName)
        {
            if (footerName == null)
                throw new ArgumentNullException(nameof(footerName));
            var result = new FooterComment<T>(footerName);

            column.Footer = result;
            return result;
        }

        public static FooterCell<T> Format<T>(this FooterCell<T> footer, string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));
            footer.Format = format;
            return footer;
        }

        #region Sum

        public static FooterCell<decimal> Sum(this IColumnSetting<decimal> column)
        {
            column.Footer = new SumDecimalCell();
            return column.Footer;
        }

        public static FooterCell<int> Sum(this IColumnSetting<int> column)
        {
            column.Footer = new Int32Cell();
            return column.Footer;
        }

        public static FooterCell<long> Sum(this IColumnSetting<long> column)
        {
            column.Footer = new SumInt64Cell();
            return column.Footer;
        }

        public static FooterCell<double> Sum(this IColumnSetting<double> column)
        {
            column.Footer = new SumDoubleCell();
            return column.Footer;
        }

        public static FooterCell<float> Sum(this IColumnSetting<float> column)
        {
            column.Footer = new SumSingleCell();
            return column.Footer;
        }

        #endregion

        #region AVG

        public static FooterCell<decimal> Avg(this IColumnSetting<decimal> column)
        {
            column.Footer = new AvgDecimalCell();
            return column.Footer;
        }

        public static FooterCell<int> Avg(this IColumnSetting<int> column)
        {
            column.Footer = new AvgInt32Cell();
            return column.Footer;
        }

        public static FooterCell<long> Avg(this IColumnSetting<long> column)
        {
            column.Footer = new AvgInt64Cell();
            return column.Footer;
        }

        public static FooterCell<double> Avg(this IColumnSetting<double> column)
        {
            column.Footer = new AvgDoubleCell();
            return column.Footer;
        }

        public static FooterCell<float> Avg(this IColumnSetting<float> column)
        {
            column.Footer = new AvgSingleCell();
            return column.Footer;
        }

        #endregion
    }
}