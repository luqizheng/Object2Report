using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coder.Object2Report;
using Coder.Object2Report.Renders.NPOI;
using UnitTest.Helper;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            HssfExcelWrite();
        }
        private static IList<Order> _orders = new List<Order>
        {
            new Order {Discount = 0.3f, UnitPrice = 10, Quantity = 10},
            new Order {Discount = 0.7f, UnitPrice = 4.1m, Quantity = 5}
        };
        public static void  HssfExcelWrite()
        {
            var render = new HssfExcelRender(File.Open("a.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite), "Test");
            var report = BuildReport(render);

            report.Write(_orders);
        }

        private static Report<Order> BuildReport(IRender render)
        {
            var report = new Report<Order>(render);

            report.Column(item => item.UnitPrice).Format("#,0.00");
            report.Column(item => item.Quantity).Format("0").Sum();
            report.Column("Member Discount", item => item.Discount).Format("0.0%").Content("SubTotal");
            report.Column("SubTotal", item => item.UnitPrice * item.Quantity).Sum();
            report.Column("Amount Paid", item =>
            {
                var result = item.UnitPrice * item.Quantity;
                var accountOf = 1 - item.Discount;
                result = result * Convert.ToDecimal(accountOf);
                return result;
            }).Sum();
            return report;
        }
    }
}
