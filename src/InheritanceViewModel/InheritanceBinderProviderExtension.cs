using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace InheritanceViewModel
{
    public static class InheritanceBinderProviderExtension
    {
        public static IList<IModelBinderProvider> UseInheritanceBinderProvider(this IList<IModelBinderProvider> modelBinderProviders)
        {
            var complexyProviderIndex = modelBinderProviders
                .Select((p, i) => new { Provider = p, Index = i })
                .FirstOrDefault(i => i.Provider is ComplexTypeModelBinderProvider)?.Index ?? 0;

            modelBinderProviders.Insert(complexyProviderIndex, new InheritanceBinderProvider());
            return modelBinderProviders;
        }
    }
}
