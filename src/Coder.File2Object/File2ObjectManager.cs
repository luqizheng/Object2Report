using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Coder.File2Object.Columns;

namespace Coder.File2Object
{
    public abstract class File2ObjectManager<TEntity, TCell>
    {
        private static readonly Regex TempalteRegex = new Regex("\\[[\\w\\d]*?\\]");
        private readonly IList<Column<TEntity, TCell>> _columns = new List<Column<TEntity, TCell>>();
        private readonly IFileReader<TCell> _fileReader;

        protected File2ObjectManager(IFileReader<TCell> fileReader)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        /// <summary>
        /// </summary>
        public int TitleRowIndex { get; set; } = 0;

        /// <summary>
        /// </summary>
        public IEnumerable<string> Titles
        {
            get { return _columns.Select(f => f.Name); }
        }

        protected abstract TEntity Create();

        public bool TryRead(string file, out IList<ImportResultItem<TEntity>> data, out string resultFile)
        {
            var fileInfo = new FileInfo(file);
            resultFile = GetResultFile(fileInfo);
            data = Read(file);

            var hasError = false;
            foreach (var item in data)
                if (item.HasError)
                {
                    if (hasError == false)
                        hasError = true;
                    var errorMessage = item.GetErrors();
                    _fileReader.WriteTo(item.Row, _columns.Count, errorMessage);
                }

            _fileReader.Write(resultFile);
            _fileReader.Close();
            return !hasError;
        }

        public void RewriteResultFile(string resultFile, IList<ImportResultItem<TEntity>> data)
        {
            _fileReader.Open(resultFile);
            var rowIndex = this.TitleRowIndex + 1;
            var cellIndex = this._columns.Count();
            foreach (var importResult in data)
            {
                _fileReader.WriteTo(importResult.Row, cellIndex, importResult.GetErrors());
            }
            _fileReader.Write(resultFile);
        }

        private string GetResultFile(FileInfo file)
        {
            var path = Path.Combine(file.Directory.FullName, file.Name.Substring(0, file.Name.Length - file.Extension.Length) + "-结果" + file.Extension);
            var index = 1;
            while (File.Exists(path))
            {
                path = Path.Combine(file.Directory.FullName,
                    file.Name + "-结果" + index + file.Extension);
                index++;
            }

            return path;
        }

        public IList<ImportResultItem<TEntity>> Read(string file)
        {
            _fileReader.Open(file);
            CheckTitles();

            try
            {
                var result = ImportResultItems();
                return result;
            }
            finally
            {
                _fileReader.Close();
            }
        }


        private IList<ImportResultItem<TEntity>> ImportResultItems()
        {
            var result = new List<ImportResultItem<TEntity>>();
            var rowIndex = TitleRowIndex + 1;


            while (TryGetRows(rowIndex, out var cells))
            {
                var resultItem = new ImportResultItem<TEntity> { Row = rowIndex };
                var entity = resultItem.Data = Create();
                result.Add(resultItem);

                for (var index = 0; index < cells.Count; index++)
                {
                    var cell = cells[index];
                    var column = _columns[index];
                    if (cell == null)
                    {
                        //为空的时候。

                        if (column.IsRequire)
                        {
                            var errorMessage = BuildErrorMessageByTemplate(column.GetErrorMessageIfEmpty(), column);
                            resultItem.AddError(index, errorMessage);
                        }
                        else
                        {
                            column.SetEmptyOrNull(entity);
                        }
                    }
                    else
                    {
                        if (!column.TrySetValue(entity, cell, out var errorMessage))
                        {
                            errorMessage = BuildErrorMessageByTemplate(errorMessage, column);
                            resultItem.AddError(index, errorMessage);
                        }
                    }
                }

                rowIndex++;
            }

            return result;
        }

        private string BuildErrorMessageByTemplate(string errorMessage, Column<TEntity, TCell> column)
        {
            return TempalteRegex.Replace(errorMessage, f =>
            {
                switch (f.Value)
                {
                    case ColumnTemplateDefined.ColumnName:
                        return column.Name;
                }

                return f.Value;
            });
            ;
        }

        private bool TryGetRows(int rowIndex, out IList<TCell> result)
        {
            result = new List<TCell>(_columns.Count);
            var emptyRow = true;
            for (var cellIndex = 0; cellIndex < _columns.Count; cellIndex++)
                if (_fileReader.TryRead(rowIndex, cellIndex, out var cell))
                {
                    result.Add(cell);
                    emptyRow = false;
                }
                else
                {
                    result.Add(default);
                }

            return !emptyRow;
        }

        private void CheckTitles()
        {
            var titles = ReadTitles();
            var index = 0;
            foreach (var settingTitle in Titles)
            {
                var fileTitle = titles[index];

                if (settingTitle != fileTitle) throw new TitleNotMatchSettingException();

                index++;
            }
        }

        private List<string> ReadTitles()
        {
            var result = new List<string>();
            for (var i = 0; i < _columns.Count; i++)
                if (_fileReader.TryRead(TitleRowIndex, i, out var cell))
                    result.Add(_fileReader.Convert(cell));
                else
                    break;

            return result;
        }

        public void Add(Column<TEntity, TCell> column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            _columns.Add(column);
        }
    }
}