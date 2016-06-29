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
            if (currentPosition.Index == 0)
            {
                Table.Add(new List<object>());
            }
            Table[currentPosition.RowIndex].Add(v);
        }

        public override void WriteFooterCell(ReportCell currentPosition, object v, string format)
        {
            if (currentPosition.Index == 0)
            {
                Table.Add(new List<object>());
            }
            Table[currentPosition.RowIndex].Add(v);
        }

        public override void WriteBodyCell(ReportCell currentPosition, object v, string format)
        {
            if (currentPosition.Index == 0)
            {
                Table.Add(new List<object>());
            }
            Table[currentPosition.RowIndex].Add(v);
        }
    }
}