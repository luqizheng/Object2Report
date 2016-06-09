using System.Collections.Generic;
using UnitTest.Helper;
using Xunit;

namespace UnitTest
{
    public class ReportTest
    {
        private readonly MemoryRender render = new MemoryRender();

        private IList<Order> _orders = new List<Order>
        {
            new Order {Amount = 100m},
            new Order {Amount = 10m}
        };

        private class Test
        {
            public object model;
        }

        [Fact]
        public void TestMethod1()
        {
            IList<Order> _orders = new List<Order>
            {
                new Order {Amount = 100m, UnitPrice = 10, Quantity = 10},
                new Order {Amount = 18m, UnitPrice = 4.1m, Quantity = 5}
            };
            var report = new Report<Order>(render);

            report.Column(item => item.UnitPrice);
            report.Column(item => item.Quantity);
            report.Column("合计", item => item.UnitPrice*item.Quantity).Sum();
            report.Column(item => item.Amount).Sum();

            report.WriteHeader();

            Assert.Equal("单价", render.Table[0][0]);
            Assert.Equal("数量", render.Table[0][1]);
            Assert.Equal("合计", render.Table[0][2]);
            Assert.Equal("实际支付", render.Table[0][3]);

            report.WriteBody(_orders);
            Assert.Equal("10", render.Table[1][0]);
            Assert.Equal(10.ToString(), render.Table[1][1]);
            Assert.Equal(100m.ToString(), render.Table[1][2]);
            Assert.Equal(100m.ToString(), render.Table[1][3]);


            Assert.Equal(4.1m.ToString(), render.Table[2][0]);
            Assert.Equal(5.ToString(), render.Table[2][1]);
            Assert.Equal((4.1m*5).ToString(), render.Table[2][2]);
            Assert.Equal(18m.ToString(), render.Table[2][3]);

            report.WriteFooter();

            Assert.Equal("", render.Table[3][0]);
            Assert.Equal("", render.Table[3][1]);
            Assert.Equal(20.5m, render.Table[3][2]);
            Assert.Equal(18m, render.Table[3][3]);
        }
    }
}