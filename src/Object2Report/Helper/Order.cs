using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object2Report.Helper
{
    public class Order
    {
        [DisplayName("购买价格")]
        public decimal Amount { get; set; }
    }
}
