using System;
using System.Collections.Generic;
using System.IO;
using Coder.Object2Report;
using Coder.Object2Report.Renders;
using Coder.Object2Report.Renders.Excel;
using UnitTest.Helper;
using Xunit;

namespace UnitTest
{
    public class ReportTest
    {
        private readonly MemoryRender _render = new MemoryRender();

        private readonly IList<Order> _orders = new List<Order>
        {
            new Order {Discount = 0.3f, UnitPrice = 10, Quantity = 10},
            new Order {Discount = 0.7f, UnitPrice = 4.1m, Quantity = 5}
        };

        private Report<Order> BuildReport(IRender render)
        {
            var report = new Report<Order>(render);

            report.Column(item => item.UnitPrice).Format("#,0.00");
            report.Column(item => item.Quantity).Format("0").Sum().Format("0");
            report.Column("Member Discount", item => item.Discount).Format("0.0%").Comment("SubTotal");
            report.Column("SubTotal", item => item.UnitPrice*item.Quantity).Sum();
            report.Column("Amount Paid", item =>
            {
                var result = item.UnitPrice*item.Quantity;
                var accountOf = 1 - item.Discount;
                result = result*Convert.ToDecimal(accountOf);
                return result;
            }).Sum();
            return report;
        }

        [Fact]
        public void CsvWrite()
        {
            var render = new CsvRender(File.OpenWrite("a.csv"));
            var report = BuildReport(render);
            report.Write(_orders);
        }

        [Fact]
        public void HssfExcelWrite()
        {
            var render = new HssfExcelRender(File.Open("a.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite), "Test");
            var report = BuildReport(render);

            report.Write(_orders);
        }

        [Fact]
        public void MarkDownWrite()
        {
            var render = new MarkDownRender(File.OpenWrite("a.md"));
            var report = BuildReport(render);
            report.Write(_orders);
        }

        [Fact]
        public void TestMethod1()
        {
            var report = BuildReport(_render);


            report.WriteHeader();

            Assert.Equal("Product-Price", _render.Table[0][0]);
            Assert.Equal("Quantity", _render.Table[0][1]);
            Assert.Equal("Member Discount", _render.Table[0][2]);
            Assert.Equal("SubTotal", _render.Table[0][3]);
            Assert.Equal("Amount Paid", _render.Table[0][4]);

            report.WriteBody(_orders);
            Assert.Equal(10m, _render.Table[1][0]);
            Assert.Equal(10, _render.Table[1][1]);
            Assert.Equal(0.3f, _render.Table[1][2]);
            Assert.Equal(100m, _render.Table[1][3]);
            Assert.Equal(70m, _render.Table[1][4]);


            Assert.Equal(4.1m, _render.Table[2][0]);
            Assert.Equal(5, _render.Table[2][1]);
            Assert.Equal(0.7f, _render.Table[2][2]);
            Assert.Equal(4.1m*5, _render.Table[2][3]);
            Assert.Equal(6.15m, _render.Table[2][4]);

            report.WriteFooter();

            Assert.Equal(15, _render.Table[3][0]);
            Assert.Equal("SubTotal", _render.Table[3][1]);
            Assert.Equal(120.5m, _render.Table[3][2]);
            Assert.Equal(76.15m, _render.Table[3][3]);
        }

        [Fact]
        public void XssfExcelWrite()
        {
            var render = new XssfExcelReader(File.Open("a.xlsx", FileMode.OpenOrCreate, FileAccess.ReadWrite), "Test");
            var report = BuildReport(render);

            report.Write(_orders);
        }
    }
}