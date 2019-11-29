using System;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Coder.File2Object.Readers
{
    public class ExcelFileReader : IFileReader<ICell>
    {
        /// <summary>
        /// </summary>
        private readonly int _sheetIndex;

        private ISheet _sheet;
        private IWorkbook _workbook;
        private bool isXSSFile;

        /// <summary>
        /// </summary>
        /// <param name="sheetIndex"></param>
        public ExcelFileReader(int sheetIndex = 0)
        {
            sheetIndex = 0;
        }

        public void Open(string file)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentException("message", nameof(file));

            _workbook = GetWorkbook(file);
            _sheet = _workbook.GetSheetAt(_sheetIndex);
        }

        public void Close()
        {
        }

        public bool TryRead(int rowIndex, int cellIndex, out ICell cell)
        {
            var row = _sheet.GetRow(rowIndex);
            cell = null;
            if (row == null) return false;


            cell = row.GetCell(cellIndex, MissingCellPolicy.RETURN_NULL_AND_BLANK);

            return cell != null;
        }

        public void Write(string file)
        {
            if (file is null) throw new ArgumentNullException(nameof(file));

            using var writeStream = File.Open(file, FileMode.Create, FileAccess.ReadWrite);
            _workbook.Write(writeStream);
            writeStream.Flush();
            writeStream.Close();
        }

        public void WriteTo(int rowIndex, int cellIndex, string value)
        {
            var row = _sheet.GetRow(rowIndex);
            var cell = row.GetCell(cellIndex, MissingCellPolicy.CREATE_NULL_AS_BLANK);
            cell.SetCellValue(isXSSFile
                ? (IRichTextString) new XSSFRichTextString(value)
                : new HSSFRichTextString(value));
        }

        public string Convert(ICell cell)
        {
            cell.SetCellType(CellType.String);
            return cell.StringCellValue;
        }

        private IWorkbook GetWorkbook(string file)
        {
            var fileStream = File.OpenRead(file);
            isXSSFile = file.EndsWith("xlsx");
            var workbook = isXSSFile
                ? (IWorkbook) new XSSFWorkbook(fileStream)
                : new HSSFWorkbook(new POIFSFileSystem(fileStream));
            fileStream.Close();
            return workbook;
        }
    }
}