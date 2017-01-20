using System.Collections.Generic;
using Coder.Object2Report;

namespace Object2Report.UnitTest.Helper
{
    public class MemoryRender : RenderBase
    {
        public MemoryRender()
        {
            Table = new List<List<object>>();
        }

        public IList<List<object>> Table { get; set; }


        public override void WriteHeader(CellCursor cellCursor, string title,string format)
        {
           
            Table[cellCursor.RowIndex].Add(title);
        }

        public override void OnRowWriting(CellCursor report, int rowIndex)
        {
            Table.Add(new List<object>());
        }

        public override void WriteFooterCell<T>(CellCursor currentPosition, T v, string format)
        {
            Table[currentPosition.RowIndex].Add(v);
        }

        public override void WriteBodyCell<T>(CellCursor currentPosition, T v, string format)
        {
          
            Table[currentPosition.RowIndex].Add(v);
        }
    }
}