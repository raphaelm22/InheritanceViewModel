using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace InheritanceViewModel
{
    public class InheritanceBinder : IModelBinder
    {
        private readonly ModelBinderProviderContext _modelBinderProviderContext;
        private readonly InheritanceBinderAttribute _inheritanceBinderAttribute;
        public InheritanceBinder(ModelBinderProviderContext modelBinderProviderContext, 
            InheritanceBinderAttribute inheritanceBinderAttribute)
        {
            _modelBinderProviderContext = modelBinderProviderContext;
            _inheritanceBinderAttribute = inheritanceBinderAttribute;
        }

        //Ref: https://github.com/aspnet/Mvc/blob/fc4098541263092ee8060589d1409841b9ca2f65/src/Microsoft.AspNetCore.Mvc.Core/ModelBinding/Binders/ComplexTypeModelBinder.cs
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var inheritanceType = ResolveInheritedViewModel(bindingContext);
            if (inheritanceType == null)
                return;

            var _modelMetadataProvider = bindingContext.HttpContext.RequestServices.GetService<IModelMetadataProvider>();
            bindingContext.ModelMetadata = _modelMetadataProvider.GetMetadataForType(inheritanceType);

            var propertyBinders = GetPropertyBinders(bindingContext.ModelMetadata);
            var binder = new ComplexTypeModelBinder(propertyBinders);

            await binder.BindModelAsync(bindingContext);

            ForceValidate(bindingContext);
        }

        private Type ResolveInheritedViewModel(ModelBindingContext bindingContext)
        {
            var resolve = Activator.CreateInstance(_inheritanceBinderAttribute.ResolveType) as IInheritanceBinderResolve;
            return resolve.Resolve(bindingContext);
        }

        private Dictionary<ModelMetadata, IModelBinder> GetPropertyBinders(ModelMetadata metadata)
        {
            var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>();
            for (var i = 0; i < metadata.Properties.Count; i++)
            {
                var property = metadata.Properties[i];
                propertyBinders.Add(property, _modelBinderProviderContext.CreateBinder(property));
            }

            return propertyBinders;
        }

        private static void ForceValidate(ModelBindingContext bindingContext)
        {
            if (bindingContext.Result.IsModelSet)
            {
                var _validator = bindingContext.HttpContext.RequestServices.GetService<IObjectModelValidator>();

                _validator.Validate(
                         bindingContext.ActionContext,
                         bindingContext.ValidationState,
                         bindingContext.ModelName,
                         bindingContext.Result.Model);
            }
        }
    }
}
