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
#if NET45
        public CsvRender(Stream writer) : this(writer, Encoding.Default)
        {
        }
#endif

        public override void WriteHeader(CellCursor cellCursor, string title, string fromat)
        {
            Write(cellCursor, title);
        }

        public override void WriteBodyCell<T>(CellCursor currentPosition, T v, string format)
        {
            Write(currentPosition, string.Format(GetFormatPatten(format), v));
        }

        private string GetFormatPatten(string format)
        {
            if (string.IsNullOrEmpty(format))
                return "{0}";
            if (format.Contains("{"))
            {
                return format;
            }
            return "{0:" + format + "}";
        }

        public override void WriteFooterCell<T>(CellCursor currentPosition, T v, string format)
        {
            Write(currentPosition, string.Format(GetFormatPatten(format), v));
        }

        public override void OnRowWriting(CellCursor cellCursor, int rowIndex)
        {
            _curRows = new string[cellCursor.MaxCell];
        }

        private void Write(CellCursor currentPosition, object v)
        {
            if (v == null)
                return;
            var value = v.ToString();
            if (value.Contains("\""))
            {
                value = $"\"{value.Replace("\"", "\"\"")}\"";
            }
            if (value.Contains(","))
            {
                value = "\"" + value + "\"";
            }
            _curRows[currentPosition.Index] = value;
        }

        public override void OnRowWrote()
        {
            var s = string.Join(",", _curRows);
            _writer.WriteLine(s);
        }

        public override void OnReportWrote()
        {
            _writer.Flush();

        }
    }
}