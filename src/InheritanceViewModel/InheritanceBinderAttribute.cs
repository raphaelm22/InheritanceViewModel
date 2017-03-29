using System;
using System.Reflection;

namespace InheritanceViewModel
{
    public sealed class InheritanceBinderAttribute : Attribute
    {
        public Type ResolveType { get; private set; }

        public InheritanceBinderAttribute(Type resolveType)
        {
            if (!typeof(IInheritanceBinderResolve).IsAssignableFrom(resolveType))
                throw new ArgumentException(nameof(ResolveType));

            ResolveType = resolveType;
        }
    }
}
