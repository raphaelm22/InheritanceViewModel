using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace InheritanceViewModel
{
    //Ref: https://github.com/aspnet/Mvc/blob/fc4098541263092ee8060589d1409841b9ca2f65/src/Microsoft.AspNetCore.Mvc.Core/ModelBinding/Binders/ComplexTypeModelBinderProvider.cs
    public class InheritanceBinderProvider : IModelBinderProvider
    {
        internal InheritanceBinderProvider() { }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.IsComplexType && !context.Metadata.IsCollectionType)
            {

                var typeInfo = context.Metadata.ModelType.GetTypeInfo();

                var inheritanceBinderAttribute = typeInfo.GetCustomAttribute<InheritanceBinderAttribute>();
                if (inheritanceBinderAttribute != null)
                    return new InheritanceBinder(context, inheritanceBinderAttribute);
            }

            return null;
        }
    }
}
