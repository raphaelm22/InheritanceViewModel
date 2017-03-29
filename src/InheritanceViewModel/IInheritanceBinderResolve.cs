using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace InheritanceViewModel
{
    public interface IInheritanceBinderResolve
    {
        Type Resolve(ModelBindingContext bindingContext);
    }
}
