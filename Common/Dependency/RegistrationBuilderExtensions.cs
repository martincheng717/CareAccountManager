using Autofac;
using Autofac.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Dependency
{
    [ExcludeFromCodeCoverage]
    public static class RegistrationBuilderExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
            CustomPropertiesAutowired<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, PropertyWiringOptions wiringFlags = PropertyWiringOptions.None)
        {
            var allowCircularDependencies = 0 != (int)(wiringFlags & PropertyWiringOptions.AllowCircularDependencies);
            var preserveSetValues = 0 != (int)(wiringFlags & PropertyWiringOptions.PreserveSetValues);
            
            if (allowCircularDependencies)
                registration.RegistrationData.ActivatedHandlers.Add((s, e) => Gdot.Care.Common.Dependency.AutowiringPropertyInjector.InjectProperties(e.Context, e.Instance, !preserveSetValues));
            else
                registration.RegistrationData.ActivatingHandlers.Add((s, e) => Gdot.Care.Common.Dependency.AutowiringPropertyInjector.InjectProperties(e.Context, e.Instance, !preserveSetValues));

            return registration;
        }
    }
}
