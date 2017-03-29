using InheritanceViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;


namespace Sample1.ViewModels.Order
{
    public class PaymentViewModelBaseResolve : IInheritanceBinderResolve
    {
        public Type Resolve(ModelBindingContext bindingContext)
        {
            var key = $"{bindingContext.ModelName}.{nameof(PaymentViewModelBase.PaymentType)}";
            var tipo = bindingContext.ValueProvider.GetValue(key);

            if (tipo == ValueProviderResult.None)
                throw new Exception($"The form must contain the property {nameof(PaymentViewModelBase.PaymentType)} to do a bind");

            EPaymentType paymentType;
            if (!Enum.TryParse(tipo.FirstValue, out paymentType))
                throw new Exception($"The property {nameof(PaymentViewModelBase.PaymentType)} don't have the supported value");

            switch (paymentType)
            {
                case EPaymentType.CreditCard:
                    return typeof(PaymentByCreditCardViewModel);
                case EPaymentType.Gold:
                    return typeof(PaymentByGoldViewModel);
                default:
                    throw new Exception($"The type {paymentType} not been mapped");
            }
        }
    }
}
