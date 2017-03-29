using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample1.Models.Product;
using Sample1.ViewModels.Order;
using Sample1.Models.Common;
using Sample1.Models.Payment;

namespace Sample1.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductStore _productStore;
        private readonly PaymentStore _paymentStore;

        public OrderController(ProductStore productStore, PaymentStore paymentStore)
        {
            _productStore = productStore;
            _paymentStore = paymentStore;
        }

        public IActionResult Buy(int id)
        {
            var productModel = _productStore.FindById(id);

            var viewModel = new OrderViewModel
            {
                Product = new ProductViewModel(productModel),
                Payment = (productModel.CostType == ECostType.Dollar
                    ? (PaymentViewModelBase)new PaymentByCreditCardViewModel()
                    : (PaymentViewModelBase)new PaymentByGoldViewModel())
            };


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Buy(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var paymentModel = viewModel.Payment.ToModel();

            if (!paymentModel.CheckOut())
            {
                ModelState.AddModelError("payment", "Check-Out failure. Please check the data and try again");
                return View(viewModel);
            }

            _paymentStore.Save(paymentModel);

            return View("Success", paymentModel.Id);
        }

    }
}
