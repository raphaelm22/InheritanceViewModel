using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample1.ViewModels.Order
{
    public class OrderViewModel
    {
        public ProductViewModel Product { get; set; }

        public PaymentViewModelBase Payment { get; set; }
    }
}
