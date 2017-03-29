namespace Sample1.Models.Payment
{
    public class PaymentByGoldModel : IPaymentModel
    {
        public int Id { get; set; }
        public string AccountId { get; set; }

        public string AccountToken { get; set; }

        public bool CheckOut()
        {
            return AccountToken.EndsWith("9");
        }
    }
}
