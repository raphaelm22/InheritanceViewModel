namespace Sample1.Models.Payment
{
    public class PaymentStore
    {
        public void Save(IPaymentModel model)
        {
            model.Id = model.GetHashCode();
        }
    }
}
