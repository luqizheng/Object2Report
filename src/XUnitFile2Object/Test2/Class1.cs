using System.ComponentModel.DataAnnotations;
using Coder.File2Object;
using Coder.File2Object.Columns;

namespace XUnitFile2Object.Test2
{
    public enum A
    {
        a1,
        a2
    }

    public enum B
    {
        [Display(Name = "b-1")] AB1,
        [Display(Name = "b-2")] AB2
    }

    public class Class1
    {
        public void TestMultiType()
        {
            var manager = new TypeTestManager();
            manager.Column(f => f.Int32);
            manager.Column(f => f.Int32Nullable);
            manager.ColumnEnumDisplayNameAttribute(f => f.B);
            manager.ColumnEnumDisplayNameAttribute(f => f.BNullable);
        }

        public class TypeTest
        {
            public int Int32 { get; set; }
            public int Int64 { get; set; }

            public int? Int32Nullable { get; set; }

            public B B { get; set; }
            public B? BNullable { get; set; }

            public A A { get; set; }
            public A? ANullable { get; set; }
        }

        public class TypeTestManager : Excel2ObjectManager<TypeTest>
        {
            protected override TypeTest Create()
            {
                return new TypeTest();
            }
        }
    }
}