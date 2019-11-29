using System.IO;

#if NETSTANDARD1_6
using Npoi.Core.SS.UserModel;
using Npoi.Core.HSSF.UserModel;
using Npoi.Core.HSSF.Util;

#else

#endif

namespace Coder.Object2Report.Renders.NPOI
{
    public class ExcelXlsxReport<T> : Report<T>
    {
        public ExcelXlsxReport(Stream stream, string sheetName, string templateFile) : base(
            new XssfExcelReader(stream, sheetName, templateFile))
        {
        }

        public ExcelXlsxReport(Stream stream) : base(new XssfExcelReader(stream))
        {
        }
    }
}