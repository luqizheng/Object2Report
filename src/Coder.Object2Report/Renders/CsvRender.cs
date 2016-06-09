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

        public override void WriteHeader(Cell currentPosition, object v)
        {
            Write(currentPosition, v, null);
        }

        public override void WriteBodyCell(Cell currentPosition, object v, string format)
        {
            Write(currentPosition, v, format);
        }

        public override void WriteFooterCell(Cell currentPosition, object v, string format)
        {
            Write(currentPosition, v, format);
        }

        private void Write(Cell currentPosition, object v, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                throw new ArgumentNullException(nameof(format));
            }
            var value = currentPosition.Index == 0
                ? string.Format(format, v)
                : string.Format("," + format, v);
            if (v.ToString().IndexOf("\"", StringComparison.Ordinal) != -1)
            {
                value = string.Format("\"{0}\"", value.Replace("\"", "\"\""));
            }

            _writer.Write(value);
            if (currentPosition.Index == currentPosition.MaxCell - 1)
            {
                _writer.WriteLine();
            }
        }
    }
}