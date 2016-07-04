using System.ComponentModel.DataAnnotations;

namespace UnitTest.Helper
{
    public class Order
    {
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Product-Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Discount")]
        public float Discount { get; set; }

        [Display(Name="Product")]
        public string ProductName { get; set; }
    }
    
}
