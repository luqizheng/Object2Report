using System;
using System.Collections.Generic;
using System.IO;
#if NETSTANDARD1_6
using Npoi.Core.SS.UserModel;

#else
using NPOI.HPSF;
using NPOI.SS.UserModel;
#endif



namespace Coder.Object2Report.Renders.NPOI
{
    public abstract class ExcelRender : RenderBase
    {
        private readonly IDictionary<int, ICellStyle> _bodyCellStyle = new Dictionary<int, ICellStyle>();
        private readonly IDictionary<int, ICellStyle> _footerCellStyle = new Dictionary<int, ICellStyle>();
        private readonly Stream _stream;
        private readonly string _workSheetName;

        private IRow _currentRow;
        private IDataFormat _dataFormat;
        private ICellStyle _footerStyle;
        private ICellStyle _headerStyle;
        private ExcelInfo _info;
        private IWorkbook _workbook;
        private ISheet _worksheet;

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
            _workSheetName = worksheetName;
        }

        public ExcelInfo Info
        {
            get { return _info ?? (_info = new ExcelInfo()); }
            set { _info = value; }
        }

        public ICellStyle HeaderStyle
        {
            get
            {
                if (_headerStyle == null)
                {
                    _headerStyle = WorkBook.CreateCellStyle();
                    _headerStyle.FillPattern = FillPattern.SolidForeground;
                }
                return _headerStyle;
            }
        }

        public ICellStyle FooterStyle
        {
            get
            {
                if (_footerStyle == null)
                {
                    _footerStyle = WorkBook.CreateCellStyle();
                    _footerStyle.FillPattern = FillPattern.SolidForeground;
                }
                return _footerStyle;
            }
        }

        public ISheet WorkSheet => _worksheet ?? (_worksheet = WorkBook.CreateSheet(_workSheetName));
        public IWorkbook WorkBook => _workbook ?? (_workbook = CreateWorkBook());

        private IDataFormat DataFormat => _dataFormat ?? (_dataFormat = WorkBook.CreateDataFormat());

        protected abstract IWorkbook CreateWorkBook();
        protected abstract void InitWorkbookInfo(IWorkbook book, ExcelInfo info);

        public override void OnReportWrote()
        {
            if (_info != null)
            {
                InitWorkbookInfo(WorkBook, _info);
            }

            WorkBook.Write(_stream);
            _bodyCellStyle.Clear();
        }

        public override void WriteBodyCell<T>(ReportCell currentPosition, T v, string format)
        {
            var bodyCell = Write(currentPosition, v);
            if (!String.IsNullOrEmpty(format))
            {
                bodyCell.CellStyle = GetCellStyleFrom(_bodyCellStyle, currentPosition.Index, format);
            }
        }


        private ICellStyle GetCellStyleFrom(IDictionary<int, ICellStyle> pools, int index, string format)
        {
            if (format == null)
                return null;
            if (pools.ContainsKey(index))
                return pools[index];

            var posOfFormat = DataFormat.GetFormat(format);
            var result = WorkBook.CreateCellStyle();

            result.DataFormat = posOfFormat;
            pools.Add(index, result);
            return result;
        }


        public override void WriteFooterCell<T>(ReportCell currentPosition, T v, string format)
        {
            var cell = Write(currentPosition, v);
            cell.CellStyle = String.IsNullOrEmpty(format)
                ? FooterStyle
                : GetCellStyleFrom(_footerCellStyle, currentPosition.Index, format);
        }

        public override void WriteHeader(ReportCell currentPosition, string title, string format)
        {
            var cell = Write(currentPosition, title);

            if (HeaderStyle != null)
            {
                cell.CellStyle = HeaderStyle;
            }
        }

        public override void OnRowWritting(ReportCell cell, int rowIndex)
        {
            _currentRow = WorkSheet.CreateRow(rowIndex);
        }


        private ICell Write<T>(ReportCell currentPosition, T v)
        {
            var cell = _currentRow.CreateCell(currentPosition.Index);
            SetCellValue(cell, v);
            return cell;
        }

        private void SetCellValue<T>(ICell cell, T v)
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
                cell.SetCellValue(num);
            }
            else if (valType == typeof(bool))
            {
                var value = Convert.ToBoolean(v);
                cell.SetCellValue(value);
            }
            else if (valType == typeof(char))
            {
                var value = Convert.ToChar(v);
                cell.SetCellValue(value);
            }
            else
            {
                cell.SetCellValue(v.ToString());
            }
        }
    }
}