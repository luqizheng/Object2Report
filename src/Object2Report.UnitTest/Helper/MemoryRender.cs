using System.Collections.Generic;
using Coder.Object2Report;

namespace UnitTest.Helper
{
    public class MemoryRender : RenderBase
    {
        public MemoryRender()
        {
            Table = new List<List<object>>();
        }

        public IList<List<object>> Table { get; set; }


        public override void WriteHeader(ReportCell currentPosition, string title,string format)
        {
           
            Table[currentPosition.RowIndex].Add(title);
        }

        public override void OnRowWritting(ReportCell report, int rowIndex)
        {
            Table.Add(new List<object>());
        }

        public override void WriteFooterCell<T>(ReportCell currentPosition, T v, string format)
        {
            Table[currentPosition.RowIndex].Add(v);
        }

        public override void WriteBodyCell<T>(ReportCell currentPosition, T v, string format)
        {
          
            Table[currentPosition.RowIndex].Add(v);
        }
    }
}