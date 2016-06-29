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
            new Order {Amount = 100m, UnitPrice = 10, Quantity = 10},
            new Order {Amount = 18m, UnitPrice = 4.1m, Quantity = 5}
        };

        [Fact]
        public void CsvWrite()
        {
            var report = new Report<Order>(new CsvRender(File.OpenWrite("a.csv")));

            report.Column(item => item.UnitPrice);
            report.Column(item => item.Quantity);
            report.Column("合计", item => item.UnitPrice*item.Quantity).Sum();
            report.Column(item => item.Amount).Sum();

            report.Write(_orders);

            report.Render = new ExcelRender(File.Open("a.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite), "Test");
            report.Write(_orders);
        }
        [Fact]
        public void ExcelWrite()
        {
            var report = new Report<Order>(new ExcelRender(File.Open("a.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite), "Test"));

            report.Column(item => item.UnitPrice);
            report.Column(item => item.Quantity).FooterName("合计");
            report.Column("合计", item => item.UnitPrice * item.Quantity).Sum();
            report.Column(item => item.Amount).Sum();

            report.Write(_orders);

     
        }
        [Fact]
        public void TestMethod1()
        {
            var report = new Report<Order>(_render);

            report.Column(item => item.UnitPrice);
            report.Column(item => item.Quantity);
            report.Column("合计", item => item.UnitPrice*item.Quantity).Sum();
            report.Column(item => item.Amount).Sum();

            report.WriteHeader();

            Assert.Equal("单价", _render.Table[0][0]);
            Assert.Equal("数量", _render.Table[0][1]);
            Assert.Equal("合计", _render.Table[0][2]);
            Assert.Equal("实际支付", _render.Table[0][3]);

            report.WriteBody(_orders);
            Assert.Equal(10m, _render.Table[1][0]);
            Assert.Equal(10m, _render.Table[1][1]);
            Assert.Equal(100m, _render.Table[1][2]);
            Assert.Equal(100m, _render.Table[1][3]);


            Assert.Equal(4.1m, _render.Table[2][0]);
            Assert.Equal(5m, _render.Table[2][1]);
            Assert.Equal((4.1m*5), _render.Table[2][2]);
            Assert.Equal(18m, _render.Table[2][3]);

            report.WriteFooter();

            Assert.Equal("", _render.Table[3][0]);
            Assert.Equal("", _render.Table[3][1]);
            Assert.Equal(120.5m, _render.Table[3][2]);
            Assert.Equal(118m, _render.Table[3][3]);
        }
    }
}