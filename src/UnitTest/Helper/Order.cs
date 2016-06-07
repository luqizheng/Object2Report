using System.ComponentModel.DataAnnotations;

namespace UnitTest.Helper
{
    public class Order
    {
        [Display(Name = "购买价")]
        public decimal Amount { get; set; }

        [Display(Name = "建议价格")]
        public decimal SuggestAmount { get; set; }

        [Display(Name = "数量")]
        public decimal Quantity { get; set; }
    }
}
