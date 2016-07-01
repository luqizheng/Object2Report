using System;
using System.IO;
using System.Text;

namespace Coder.Object2Report.Renders
{
    /*
     * 1 开头是不留空，以行为单位。
     * 2 可含或不含列名，含列名则居文件第一行。
     * 3 一行数据不跨行，无空行。
     * 4 以半角逗号（即,）作分隔符，列为空也要表达其存在。
     * 5 列内容如存在半角逗号（即,）则用半角双引号（即""）将该字段值包含起来。
     * 6 列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
     * 7 文件读写时引号，逗号操作规则互逆。
     * 8 内码格式不限，可为 ASCII、Unicode 或者其他。
     * 9 不支持特殊字符*/

    public class CsvRender : RenderBase
    {
        private readonly StreamWriter _writer;

        private string[] _curRows;

        public CsvRender(Stream writer, Encoding encoding)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            _writer = new StreamWriter(writer, encoding);
        }

        public CsvRender(Stream writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            _writer = new StreamWriter(writer, Encoding.UTF8);
        }

        public override void WriteHeader(ReportCell currentPosition, object v)
        {
            Write(currentPosition, v);
        }

        public override void WriteBodyCell(ReportCell currentPosition, object v, string format)
        {
            Write(currentPosition, v);
        }

        public override void WriteFooterCell(ReportCell currentPosition, object v, string format)
        {
            Write(currentPosition, v);
        }

        public override void OnRowWritting(Report report, int rowIndex)
        {
            _curRows = new string[report.Columns.Count];
        }

        private void Write(ReportCell currentPosition, object v)
        {
            if (v == null)
                return;
            var value = v.ToString();
            if (v.ToString().IndexOf("\"", StringComparison.Ordinal) != -1)
            {
                value = $"\"{value.Replace("\"", "\"\"")}\"";
            }
            _curRows[currentPosition.Index] = value;
        }

        public override void OnRowWorte()
        {
            var s = string.Join(",", _curRows);
            _writer.WriteLine(s);
        }

        public override void OnReportWrote()
        {
            _writer.Flush();
            _writer.Dispose();
        }
    }
}