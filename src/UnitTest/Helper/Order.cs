using System.ComponentModel.DataAnnotations;

namespace UnitTest.Helper
{
    public class Order
    {
        [Display(Name = "实际支付")]
        public decimal Amount { get; set; }

        [Display(Name = "建议价格")]
        public decimal SuggestAmount { get; set; }

        [Display(Name = "数量")]
        public decimal Quantity { get; set; }
        [Display(Name = "单价")]
        public decimal UnitPrice { get; set; }
    }
}
