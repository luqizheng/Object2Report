using System;
using Coder.Object2Report;
using System.Collections.Generic;
namespace UnitTest.Helper
{
    public class MemoryRender : Coder.Object2Report.RenderBase
    {
        public MemoryRender()
        {
            Table = new List<List<object>>();
        }
        public IList<List<object>> Table { get; set; }



        public override void WriteHeader(Cell currentPosition, object v)
        {
            if(currentPosition.Index==0)
            {
                Table.Add(new List<object>());
            }
            Table[currentPosition.RowIndex].Add(v);
        }

        public override void WriteFooterCell(Cell currentPosition, object v, string format)
        {
            if (currentPosition.Index == 0)
            {
                Table.Add(new List<object>());
            }
            Table[currentPosition.RowIndex].Add(String.Format(format??"{0}",v));
        }
        public override void WriteBodyCell(Cell currentPosition, object v, string format)
        {
            if (currentPosition.Index == 0)
            {
                Table.Add(new List<object>());
            }
            Table[currentPosition.RowIndex].Add(String.Format(format ?? "{0}", v));
        }
    }
}
