using Sample1.Models.Payment;
using System.ComponentModel.DataAnnotations;
using System;

namespace Sample1.ViewModels.Order
{
    public class PaymentByCreditCardViewModel : PaymentViewModelBase
    {
        public override EPaymentType PaymentType => EPaymentType.CreditCard;

        [Display(Name = "Name of credit card owner")]
        [Required()]
        [StringLength(100)]
        [RegularExpression("(?i)^[a-z ]+$")]
        public string CardOwnerName { get; set; }

        [Display(Name = "Number of credit card")]
        [Required()]
        [StringLength(16)]
        [RegularExpression("(?i)[0-9]{16}$")]
        public string CardNumber { get; set; }

        public override IPaymentModel ToModel()
        {
            return new PaymentByCreditCardModel
            {
                CardOwnerName = CardOwnerName,
                CardNumber = CardNumber
            };
        }
    }
}
