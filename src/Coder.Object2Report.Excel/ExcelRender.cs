using System;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace Coder.Object2Report.Renders.Excel
{
    /// <summary>
    /// By NPOI HSSF Excel.
    /// </summary>
    public class ExcelRender : RenderBase
    {
        private readonly Stream _stream;
        private readonly HSSFWorkbook _workbook;
        private readonly ISheet _worksheet;

        private IRow _currentRow;

        private ExcelInfo _info;

        public ExcelRender(Stream stream, string worksheetName)
        {
            _stream = stream;
            _workbook = new HSSFWorkbook();
            _worksheet = _workbook.CreateSheet(worksheetName);

            HeaderStyle = _workbook.CreateCellStyle();
            HeaderStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            HeaderStyle.FillPattern = FillPattern.SolidForeground;

            FooterStyle = _workbook.CreateCellStyle();
            FooterStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            FooterStyle.FillPattern = FillPattern.SolidForeground;

        }

        public ExcelInfo Info
        {
            get { return _info ?? (_info = new ExcelInfo()); }
            set { _info = value; }
        }

        public ICellStyle HeaderStyle { get; set; }
        public ICellStyle FooterStyle { get; set; }


        public override void OnReportWrote()
        {
            if (_info != null)
            {
                _workbook.DocumentSummaryInformation = _info.CreateDocumentInfo();
                _workbook.SummaryInformation = _info.CreateWorkBookInfo();
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
                cell.SetCellValue((bool)v);
            }
            else if (valType == typeof(char))
            {
                cell.SetCellValue((char)v);
            }
            else
            {
                cell.SetCellValue(v.ToString());
            }
        }
    }
}