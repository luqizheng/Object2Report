
## Defined output format.

I have a order class like:
```
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

```
Defined format and output them by Coder.Object2Report.Renders.CSVRender

```
var report = new Report<Order>(new CsvRender(File.OpenWrite("a.csv")));
report.Column(item => item.UnitPrice); 
report.Column(item => item.Quantity);
report.Column("合计", item => item.UnitPrice*item.Quantity).Sum();  //sum the Column and output it on the footer
report.Column(item => item.Amount).Sum();
report.Write(_orders);

```

```  CSV-File
单价,数量,合计,实际支付
10,10,100,100
4.1,5,20.5,18
,,120.5,118

```

Done.

