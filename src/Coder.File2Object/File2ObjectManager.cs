using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coder.File2Object.Columns;

namespace Coder.File2Object
{
    public abstract class File2ObjectManager<TEntity, TCell>
    {
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
        public IEnumerable<string> Titles { get; set; }

        protected abstract TEntity Create();

        public bool TryRead(string file, out IList<ImportResultItem<TEntity>> data, out string resultFile)
        {
            var fileInfo = new FileInfo(file);
            resultFile = GetResultFile(fileInfo);
            data = Read(file, out var hasError);
            _fileReader.Write(resultFile);
            return hasError;
        }

        private string GetResultFile(FileInfo file)
        {
            var path = Path.Combine(file.Directory.FullName, file.Name + "-结果" + file.Extension);
            var index = 1;
            while (File.Exists(path))
            {
                path = Path.Combine(file.Directory.FullName,
                    file.Name + "-结果" + index + file.Extension);
                index++;
            }

            return path;
        }

        public IList<ImportResultItem<TEntity>> Read(string file, out bool hasError)
        {
            _fileReader.Open(file);
            CheckTitles();

            try
            {
                var result = ImportResultItems(out hasError);
                return result;
            }
            finally
            {
                _fileReader.Close();
            }
        }

        private IList<ImportResultItem<TEntity>> ImportResultItems(out bool hasError)
        {
            var result = new List<ImportResultItem<TEntity>>();
            var rowIndex = TitleRowIndex + 1;
            hasError = false;
            var cellIndex = 0;
            var resultItem = new ImportResultItem<TEntity>();
            var entity = resultItem.Data = Create();

            var emptyCount = 0; //单emptyCount 大于 column.length值的时候，表明这一样为空。
            while (_fileReader.TryRead(rowIndex, cellIndex, out var cell))
            {
                if (cell == null)
                {
                    emptyCount++;
                    cellIndex++;
                    if (emptyCount != _columns.Count)
                        continue;
                    break;
                }

                emptyCount = 0;
                var column = _columns[cellIndex];

                if (!column.TrySetValue(entity, cell, out var errormessage))
                    resultItem.AddError(cellIndex, errormessage);


                cellIndex++;
                if (cellIndex >= _columns.Count)
                {
                    result.Add(resultItem);
                    if (resultItem.HasError) _fileReader.WriteTo(rowIndex, _columns.Count, resultItem.GetErrors());
                    resultItem = new ImportResultItem<TEntity>();
                    entity = resultItem.Data = Create();

                    rowIndex++;
                    cellIndex = 0;
                }
            }


            return result;
        }

        private void CheckTitles()
        {
            if (!Titles.Any()) return;
            var titles = ReadTitles();
            var index = 0;
            foreach (var settingTitle in Titles)
            {
                var fileTitle = titles[index];

                if (settingTitle != fileTitle) throw new Exception("文件标题不正确。");

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