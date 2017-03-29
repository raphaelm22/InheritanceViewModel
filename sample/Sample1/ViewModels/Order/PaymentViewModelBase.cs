using InheritanceViewModel;
using Sample1.Models.Payment;

namespace Sample1.ViewModels.Order
{
    [InheritanceBinder(typeof(PaymentViewModelBaseResolve))]
    public abstract class PaymentViewModelBase
    {
        public abstract EPaymentType PaymentType { get; }

        public abstract IPaymentModel ToModel();
    }
}
