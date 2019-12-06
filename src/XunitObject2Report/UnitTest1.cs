using System;
using System.Collections.Generic;
using Coder.Object2Report;
using Xunit;

namespace XunitObject2Report
{
    public class UnitTest1
    {
        public class NameTest
        {
            public string Name { get; set; }
            public decimal Decimal { get; set; }

            public int Int32 { get; set; }
            public int? Int32Nullable { get; set; }

            public long Int64 { get; set; }

            public DateTime Datetime { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var list = new List<NameTest>
            {
                new NameTest
                {
                    Decimal = 11.22m,
                    Name = "test",
                    Int32 = 30,
                    Int32Nullable=null,
                    Int64 = long.MaxValue,
                    Datetime=DateTime.Now,
        }
    };
            var report = new Report<NameTest>();
            report.Column("name", f => f.Name);
            report.Column(f => f.Decimal);
            report.Column(f => f.Name);
            report.Column("Int32", f => f.Int32);
            report.Column("Int32NullAble", f => f.Int32Nullable);
            report.Column("Int64", f => f.Int64);
            report.Column("Datetime", f => f.Datetime);
            report.Column("name", f => f.Name);
            report.Column("name", f => f.Name);

            report.WriteToXlsx(list, "a.xlsx");
        }
    }
}