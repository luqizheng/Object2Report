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
        public static FooterComment<T> FootText<T>(this IColumnSetting<T> column, string footerName)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            if (footerName == null)
                throw new ArgumentNullException(nameof(footerName));
            var result = new FooterComment<T>(footerName);

            column.Footer = result;
            return result;
        }

        public static FooterCell<T> Format<T>(this FooterCell<T> footer, string format)
        {
            if (footer == null) throw new ArgumentNullException(nameof(footer));
            if (format == null)
                throw new ArgumentNullException(nameof(format));
            footer.Format = format;
            return footer;
        }

        #region Sum

        /// <summary>
        /// </summary>
        /// <param name="column"></param>
        /// <param name="format">if format is null, it will use from column's format</param>
        /// <returns></returns>
        public static FooterCell<decimal> Sum(this IColumnSetting<decimal> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new SumDecimalCell {Format = format ?? column.Format};
            return column.Footer;
        }

        public static FooterCell<int> Sum(this IColumnSetting<int> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new Int32Cell {Format = format ?? column.Format};
            return column.Footer;
        }

        public static FooterCell<long> Sum(this IColumnSetting<long> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new SumInt64Cell {Format = format ?? column.Format};
            return column.Footer;
        }

        public static FooterCell<double> Sum(this IColumnSetting<double> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new SumDoubleCell {Format = format ?? column.Format};
            return column.Footer;
        }

        public static FooterCell<float> Sum(this IColumnSetting<float> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new SumSingleCell();
            column.Footer.Format = format ?? column.Format;
            return column.Footer;
        }

        #endregion

        #region AVG

        public static FooterCell<decimal> Avg(this IColumnSetting<decimal> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new AvgDecimalCell {Format = format ?? column.Format};
            return column.Footer;
        }

        public static FooterCell<int> Avg(this IColumnSetting<int> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new AvgInt32Cell {Format = format ?? column.Format};
            return column.Footer;
        }

        public static FooterCell<long> Avg(this IColumnSetting<long> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new AvgInt64Cell {Format = format ?? column.Format};
            return column.Footer;
        }

        public static FooterCell<double> Avg(this IColumnSetting<double> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new AvgDoubleCell();
            column.Footer.Format = format ?? column.Format;
            return column.Footer;
        }

        public static FooterCell<float> Avg(this IColumnSetting<float> column, string format = null)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            column.Footer = new AvgSingleCell();
            column.Footer.Format = format ?? column.Format;
            return column.Footer;
        }

        #endregion
    }
}