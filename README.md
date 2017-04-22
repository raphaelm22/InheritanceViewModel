# ViewModel with inheritance in ASP .NET Core

Background
-------

In payment form, there are two methods. First is using the credit card, and the other method is using generics points, in this case I called "Gold".

The initial problem
--------
I do not want create one ViewModel with all fields to reference the Credit Card and data to access the "account of Gold". Because the user only need fill the fields for Credit Card or "account of Gold". The problem is more evident when is need create custom validation. 


Solution
--------
I belive the inheritance solve that. So I propose that the ViewModel be something like this:

```C#
public abstract class PaymentViewModelBase
{
    public abstract EPaymentType PaymentType { get; }
}

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
}

public class PaymentByGoldViewModel : PaymentViewModelBase
{
    public override EPaymentType PaymentType => EPaymentType.Gold;
        
    [Display(Name = "Account ID")]
    [Required()]
    public string AccountId { get; set; }

    [Display(Name = "Account Token")]
    [Required()]
    public string AccountToken { get; set; }
}

```

The new problem
--------
Now, I have a new problem. There aren't a bind to object with inheritance. 


Solution
--------
So, I wrote a bind to resolve that. But it's a little dumb, you must manually say what the final type will be. Let's go to see.

First you decorate the abstract class with the ``InheritanceBinder`` and tells the class which will help you choose the final type of ViewModel.

```C#
[InheritanceBinder(typeof(PaymentViewModelBaseResolve))]
public abstract class PaymentViewModelBase
```

And then imply the ``Resolve`` method of the ``IInheritanceBinderResolve`` interface. 

The ``bindingContext`` attribute will probably have everything that will help you choose the final ViewModel type

```C#
public class PaymentViewModelBaseResolve : IInheritanceBinderResolve
{
    public Type Resolve(ModelBindingContext bindingContext)
    {
        var key = $"{bindingContext.ModelName}.{nameof(PaymentViewModelBase.PaymentType)}";
        var typeOfPayment = bindingContext.ValueProvider.GetValue(key);

        if (typeOfPayment == ValueProviderResult.None)
            throw new Exception($"The form must contain the property {nameof(PaymentViewModelBase.PaymentType)} to do a bind");

        EPaymentType paymentType;
        if (!Enum.TryParse(typeOfPayment.FirstValue, out paymentType))
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
```
In that case, the ViewModel has the PaymentType property, it is an enum, so depending on the value I know which end type.

The good news is when ``IsValid`` is invoked, it only validates the required properties, ie if the user informs the credit card it will only validate the ``PaymentByCreditCardViewModel`` fields
