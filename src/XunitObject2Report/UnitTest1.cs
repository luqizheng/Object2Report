using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Coder.Object2Report;
using Xunit;

namespace XunitObject2Report
{
    public class UnitTest1
    {
        public class NameTest
        {
            public string Name { get; set; }
            public decimal Decimel { get; set; }

            public int Int32 { get; set; }
        }
        [Fact]
        public void Test1()
        {
            var list = new List<NameTest>()
            {
                new NameTest()
                {
                    Decimel = 11.22m,
                    Name = "test",
                    Int32 = 30
                }
            };
            var report = new Report<NameTest>();
            report.Column("name", f => f.Name);

            report.WriteToXlsx(list, "a.xlsx");
        }
    }
}