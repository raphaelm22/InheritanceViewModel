using System;
using System.ComponentModel.DataAnnotations;
using Sample1.Models.Payment;

namespace Sample1.ViewModels.Order
{
    public class PaymentByGoldViewModel : PaymentViewModelBase
    {
        public override EPaymentType PaymentType => EPaymentType.Gold;
        
        [Display(Name = "Account ID")]
        [Required()]
        public string AccountId { get; set; }

        [Display(Name = "Account Token")]
        [Required()]
        public string AccountToken { get; set; }

        public override IPaymentModel ToModel()
        {
            return new PaymentByGoldModel
            {
                AccountId = AccountId,
                AccountToken = AccountToken
            };
        }
    }
}
