using System.IO;
using Xunit;

namespace XunitObject2Report
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var file = new FileInfo("abcd.xlst");
            var adf = file.Name.Substring(0, file.Name.Length - file.Extension.Length);
            Assert.Equal("abcd", adf);
        }
    }
}