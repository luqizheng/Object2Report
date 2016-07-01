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


        public override void WriteHeader(ReportCell currentPosition, object v)
        {
           
            Table[currentPosition.RowIndex].Add(v);
        }

        public override void OnRowWritting(Report report, int rowIndex)
        {
            Table.Add(new List<object>());
        }

        public override void WriteFooterCell(ReportCell currentPosition, object v, string format)
        {
            Table[currentPosition.RowIndex].Add(v);
        }

        public override void WriteBodyCell(ReportCell currentPosition, object v, string format)
        {
          
            Table[currentPosition.RowIndex].Add(v);
        }
    }
}