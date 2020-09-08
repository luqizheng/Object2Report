# Object2Report

Ouput CSV and MarkDown table.

## Define output format.

I have a order class like:
```
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

```
## Define render

```
// CSV render
  var render = new CsvRender(File.OpenWrite("a.csv"));
// MarkDown
 var render = new MarkDownRender(File.OpenWrite("a.md"));
// NPIO 
var render = new HssfExcelRender(File.Open("a.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite), "Test");
var render = new XssfExcelReader(File.Open("a.xlsx", FileMode.OpenOrCreate, FileAccess.ReadWrite), "Test");

```
or

```
using using Coder.Object2Report;


            var report = new Report<NameTest>();
            report.Column("name", f => f.Name);
        
            report.WriteToXlsx(list, "a.xlsx");
            report.WriteToCSV(list,"a.csv");
```

Or inerit from IRender to imple new render.



## Define output data from model 


```

var report = new Report<Order>(render);
//cell format 
report.Column(item => item.UnitPrice).Format("#,0.00"); 

//sum and output in footer cell, Format is same as cell by default. 
report.Column("Member Discount", item => item.Discount).Format("0.0%").Content("SubTotal")
report.Column(item => item.Quantity).Format("0").Sum();;
report.Column("SubTotal", item => item.UnitPrice*item.Quantity).Sum();

report.Column("Amount Paid", item =>
{
    var result = item.UnitPrice*item.Quantity;
    var accountOf = 1 - item.Discount;
    result = result*Convert.ToDecimal(accountOf);
    return result;
}).Sum();
return report;

```
##output file
```  CSV-File
Product-Price,Quantity,Member Discount,SubTotal,Amount Paid
10.00,10,30.0%,100,70.0
4.10,5,70.0%,20.5,6.15
,15,SubTotal,120.5,76.15


```

All code in the UnitTest project.

