using System;
using System.Collections.Generic;
using Coder.Object2Report;
using Microsoft.DotNet.ProjectModel;
using UnitTest.Helper;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class ReportTest
    {
        MemoryRender render = new MemoryRender();

        private IList<Order> _orders = new List<Order>()
        {

            {new Order() {Amount=100m} },
              {new Order() {Amount=10m} }

        };

        private class Test
        {
            public object model;
        }
        
        [Fact]
        public void TestMethod1()
        {
            var report = new Report<Order>(render, true);

            report.Column(item => item.Amount);
            report.Column(item => item.Amount * item.SuggestAmount);
            report.WriteHeader();
            report.WriteBody(_orders);

        }
    }
}
