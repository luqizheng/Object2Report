using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Coder.Object2Report.Footers;
using Coder.Object2Report.Footers.Sum;

namespace Coder.Object2Report
{
    public static class Footer
    {
        public static FooterColumn<T> Sum<T>(Expression<Func<T, decimal>> expression) where T : new()
        {
            return new DecimalColumn<T>(expression);
        }
    }
}
