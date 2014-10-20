namespace Kheper.Core.Dependency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;

    public class PreemptiveRegistrationSource : IRegistrationSource
    {
        private readonly EPrecedence precedence;

        private readonly IContainer autowiringContainer;

        public PreemptiveRegistrationSource(EPrecedence precedence, Assembly[] assemblies)
        {
            this.precedence = precedence;
            var builder = new ContainerBuilder();

            // TODO: what scope will be used for autowired component?
            var registration = builder.RegisterAssemblyTypes(assemblies)
                .Where(t => Attribute.IsDefined(t, typeof(ComponentAttribute)))
                .AsImplementedInterfaces();

            registration.ActivatorData.ConfigurationActions.Add(RecognizeLifetimeScope);

            this.autowiringContainer = builder.Build();
        }

        private static void RecognizeLifetimeScope(Type type, IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> rb)
        {
            var attribute = (LifetimeScopeAttribute)Attribute.GetCustomAttribute(type, typeof(LifetimeScopeAttribute));
            if (attribute != null)
            {
                var scope = attribute.Scope;
                switch (scope)
                {
                    case ELifetimeScope.SingleInstance:
                        rb.SingleInstance();
                        break;
                    case ELifetimeScope.InstancePerDependency:
                        rb.InstancePerDependency();
                        break;
                    case ELifetimeScope.InstancePerLifetimeScope:
                        rb.InstancePerLifetimeScope();
                        break;
                    case ELifetimeScope.Unspecified:
                        // decision will be made by the container
                        break;
                    default:
                        throw new NotImplementedException("Lifetime scope " + scope + " is not recognized");
                }
            }
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            // try to find existing registrations in the main container
            var items = registrationAccessor(service);
            if (items.Any())
            {
                return items;
            }

            // find registrations in autowiring container
            var registrations = this.autowiringContainer.ComponentRegistry.RegistrationsFor(service);

            IComponentRegistration foundRegistration = null;
            var foundPrecedence = EPrecedence.Unspecified;

            foreach (var registration in registrations)
            {
                Type type = registration.Activator.LimitType;
                var attribute = (ComponentAttribute)Attribute.GetCustomAttribute(type, typeof(ComponentAttribute));
                if (attribute == null)
                {
                    throw new InvalidOperationException("Found registration without Autowire attribute" + registration);
                }

                EPrecedence attributePrecedence = attribute.Precedence != EPrecedence.Unspecified ?
                    attribute.Precedence : EPrecedence.Application;

                // if registered component has precedence which is enabled for the current environement
                // and registered component has greater precedence over the other components
                // then we use this component in resolution
                if (attributePrecedence <= this.precedence && attributePrecedence > foundPrecedence)
                {
                    foundRegistration = registration;
                    foundPrecedence = attribute.Precedence;
                }
            }

            if (foundRegistration != null)
            {
                return new[] { foundRegistration };
            }
            else
            {
                return Enumerable.Empty<IComponentRegistration>();
            }
        }

        public bool IsAdapterForIndividualComponents
        {
            get { return false; }
        }
    }
}