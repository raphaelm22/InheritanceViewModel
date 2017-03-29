namespace Sample1.Models.Payment
{
    public interface IPaymentModel
    {
        int Id { get; set; }
        bool CheckOut();
    }
}
