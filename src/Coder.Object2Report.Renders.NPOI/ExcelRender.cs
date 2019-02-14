using System;
using System.Collections.Generic;
using System.IO;
#if NETSTANDARD1_6
using Npoi.Core.SS.UserModel;

#else
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
        private bool? _skipWriteTitle;
        private IWorkbook _workbook;
        private ISheet _worksheet;

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="templateFile"></param>
        /// <param name="worksheetName"></param>
        /// <exception cref="ArgumentNullException">stream is null</exception>
        protected ExcelRender(Stream stream, string worksheetName = "sheet1", string templateFile = null)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            TemplateExceFile = templateFile;
            _workSheetName = worksheetName ?? throw new ArgumentNullException(nameof(worksheetName));
        }

        public string TemplateExceFile { get; }

        /// <summary>
        /// </summary>
        public ExcelInfo Info
        {
            get => _info ?? (_info = new ExcelInfo());
            set => _info = value;
        }

  

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
        public ISheet WorkSheet
        {
            get
            {
                if (_worksheet == null)
                {
                     _worksheet = WorkBook.GetSheet(_workSheetName) ?? WorkBook.CreateSheet(_workSheetName);
                }

                return _worksheet;
            }
        }

        /// <summary>
        /// </summary>
        public IWorkbook WorkBook => _workbook ?? (_workbook = CreateWorkBook());

        private IDataFormat DataFormat => _dataFormat ?? (_dataFormat = WorkBook.CreateDataFormat());

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected abstract IWorkbook CreateWorkBook();

        /// <summary>
        /// </summary>
        /// <param name="book"></param>
        /// <param name="info"></param>
        protected abstract void InitWorkbookInfo(IWorkbook book, ExcelInfo info);

        /// <summary>
        /// </summary>
        public override void OnReportWrote()
        {
            if (_info != null) InitWorkbookInfo(WorkBook, _info);

            WorkBook.Write(_stream);
            _bodyCellStyle.Clear();
            if (_stream.CanWrite)
                _stream.Flush();
        }

        /// <summary>
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        /// <typeparam name="T"></typeparam>
        public override void WriteBodyCell<T>(CellCursor currentPosition, T v, string format)
        {
            var bodyCell = Write(currentPosition, v);
            if (!string.IsNullOrEmpty(format))
                bodyCell.CellStyle = GetCellStyleFrom(_bodyCellStyle, currentPosition.Index, format);
        }


        /// <summary>
        /// </summary>
        /// <param name="pools"></param>
        /// <param name="index"></param>
        /// <param name="format"></param>
        /// <returns></returns>
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


        /// <summary>
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <param name="format"></param>
        /// <typeparam name="T"></typeparam>
        public override void WriteFooterCell<T>(CellCursor currentPosition, T v, string format)
        {
            var cell = Write(currentPosition, v);
            cell.CellStyle = string.IsNullOrEmpty(format)
                ? FooterStyle
                : GetCellStyleFrom(_footerCellStyle, currentPosition.Index, format);
        }

        /// <summary>
        /// </summary>
        /// <param name="cellCursor"></param>
        /// <param name="title"></param>
        /// <param name="format"></param>
        public override void WriteHeader(CellCursor cellCursor, string title, string format)
        {
      
            var cell = Write(cellCursor, title);

            if (HeaderStyle != null) cell.CellStyle = HeaderStyle;
        }

        /// <summary>
        /// </summary>
        /// <param name="cellCursor"></param>
        /// <param name="rowIndex"></param>
        public override void OnRowWriting(CellCursor cellCursor, int rowIndex)
        {
            _currentRow = WorkSheet.CreateRow(rowIndex);
        }


        /// <summary>
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="v"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private ICell Write<T>(CellCursor currentPosition, T v)
        {
            var cell = _currentRow.CreateCell(currentPosition.Index);
            SetCellValue(cell, v);
            return cell;
        }

        /// <summary>
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="v"></param>
        /// <typeparam name="T"></typeparam>
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