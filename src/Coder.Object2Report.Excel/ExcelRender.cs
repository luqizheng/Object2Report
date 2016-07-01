using System;
using System.IO;
using NPOI.SS.UserModel;

namespace Coder.Object2Report.Renders.Excel
{
    public abstract class ExcelRender : RenderBase
    {
        private readonly Stream _stream;

        private IRow _currentRow;
        private ExcelInfo _info;
        private IWorkbook _workbook;
        private ISheet _worksheet;
        private readonly string _workSheetName;

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="worksheetName"></param>
        /// <exception cref="ArgumentNullException">stream is null</exception>
        protected ExcelRender(Stream stream, string worksheetName = "sheet1")
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (worksheetName == null)
                throw new ArgumentNullException(nameof(worksheetName));
            _stream = stream;
        }

        public ExcelInfo Info
        {
            get { return _info ?? (_info = new ExcelInfo()); }
            set { _info = value; }
        }

        public ICellStyle HeaderStyle { get; set; }
        public ICellStyle FooterStyle { get; set; }

        public override void OnReportWritting()
        {
            _workbook = CreateWorkBook();
            _worksheet = _workbook.CreateSheet(_workSheetName);

            HeaderStyle = _workbook.CreateCellStyle();
            HeaderStyle.FillPattern = FillPattern.SolidForeground;

            FooterStyle = _workbook.CreateCellStyle();
            FooterStyle.FillPattern = FillPattern.SolidForeground;
        }

        protected abstract IWorkbook CreateWorkBook();
        protected abstract void InitWorkbookInfo(IWorkbook book, ExcelInfo info);

        public override void OnReportWrote()
        {
            if (_info != null)
            {
                InitWorkbookInfo(_workbook, _info);
            }
            _workbook.Write(_stream);
        }

        public override void WriteBodyCell(ReportCell currentPosition, object v, string format)
        {
            Write(currentPosition, v, format);
        }

        public override void WriteFooterCell(ReportCell currentPosition, object v, string format)
        {
            var cell = Write(currentPosition, v, format);
            if (FooterStyle != null)
            {
                cell.CellStyle = FooterStyle;
            }
        }

        public override void WriteHeader(ReportCell currentPosition, object v)
        {
            var cell = Write(currentPosition, v, null);
            if (HeaderStyle != null)
            {
                cell.CellStyle = HeaderStyle;
            }
        }

        public override void OnRowWritting(Report report, int rowIndex)
        {
            _currentRow = _worksheet.CreateRow(rowIndex);
        }


        private ICell Write(ReportCell currentPosition, object v, string format)
        {
            var cell = _currentRow.CreateCell(currentPosition.Index);
            SetCellValue(cell, v, format);
            return cell;
        }

        private void SetCellValue(ICell cell, object v, string format)
        {
            if (v == null)
            {
                cell.SetCellValue("");
                return;
            }
            var valType = v.GetType();
            if (valType == typeof(decimal) || valType == typeof(int) || valType == typeof(double) ||
                valType == typeof(long) || valType == typeof(float) || valType == typeof(short))
            {
                var num = Convert.ToDouble(v);
                if (format == null)
                {
                    cell.SetCellValue(num);
                }
                else
                {
                    cell.SetCellValue(num.ToString(format));
                }
            }
            else if (valType == typeof(bool))
            {
                cell.SetCellValue((bool) v);
            }
            else if (valType == typeof(char))
            {
                cell.SetCellValue((char) v);
            }
            else
            {
                cell.SetCellValue(v.ToString());
            }
        }
    }
}