using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coder.File2Object
{
    public class ImportResultItem<T>
    {
        public T Data { get; set; }
        public int Row { get; set; }

        public IList<CellInfo> CellErrors { get; } = new List<CellInfo>();
        public IList<CellInfo> CellWarnings { get; } = new List<CellInfo>();
        public bool HasError => CellErrors.Any();

        //public T GetData<T>()
        //{
        //    return (T)Data;
        //}

        public string GetErrors(string[] titles)
        {
            var sb = new StringBuilder();
            foreach (var error in CellWarnings) sb.Append("[" + titles[error.CellIndex] + "]" + error.Message + " ");
            foreach (var error in CellErrors) sb.Append(titles[error.CellIndex] + error.Message + " ");

            var r = sb.ToString();
            return r;
        }

        public void AddWarning(int cellIndex, string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("message", nameof(message));

            CellWarnings.Add(new CellInfo
            {
                CellIndex = cellIndex,
                Message = message
            });
        }

        public void AddError(int cellIndex, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage)) throw new ArgumentException("message", nameof(errorMessage));

            CellErrors.Add(new CellInfo
            {
                CellIndex = cellIndex,
                Message = errorMessage
            });
        }
    }
}