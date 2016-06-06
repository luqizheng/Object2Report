using System;
using System.Collections.Generic;
using Coder.Object2Report;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Object2Report.Helper;

namespace Object2Report
{
    [TestClass]
    public class ReportTest
    {
        MemoryRender render = new MemoryRender();

        private IList<Order> _orders = new List<Order>()
        {

            {new Order() {Amount=100m} },
              {new Order() {Amount=10m} }

        };



        [TestMethod]
        public void TestMethod1()
        {
            var report = new Report<Order>(render, true);

            report.Column(item => item.Amount);

            report.Write(_orders);

        }
    }
}
