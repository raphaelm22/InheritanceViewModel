using System;

namespace Sample1.Models.Payment
{
    public class PaymentByCreditCardModel : IPaymentModel
    {
        public int Id { get; set; }

        public string CardOwnerName { get; set; }

        public string CardNumber { get; set; }


        public bool CheckOut()
        {
            return CardNumber.EndsWith("0");
        }
    }
}
