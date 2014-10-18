namespace Kheper.Core.Dependency
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using Autofac;
    using Autofac.Core;

    public class PreemptiveAutowiring : IRegistrationSource
    {
        private readonly EPrecedence precedence;

        private readonly Assembly[] assemblies;

        private readonly IContainer container;

        public PreemptiveAutowiring(EPrecedence precedence, Assembly[] assemblies)
        {
            this.precedence = precedence;
            this.assemblies = assemblies;
            var builder = new ContainerBuilder();

            // TODO: what scope will be used for autowired component?
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => Attribute.IsDefined(t, typeof(AutowireAttribute)))
                .As(t => ((AutowireAttribute)Attribute.GetCustomAttribute(t, typeof(AutowireAttribute))).Service);
            this.container = builder.Build();
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var items = registrationAccessor(service);
            if (items.Any())
            {
                return items;
            }

            var registrations = this.container.ComponentRegistry.RegistrationsFor(service);

            IComponentRegistration foundRegistration = null;
            var foundPrecedence = EPrecedence.Unspecified;

            foreach (var registration in registrations)
            {
                Type type = registration.Activator.LimitType;
                var attr = (AutowireAttribute)Attribute.GetCustomAttribute(type, typeof(AutowireAttribute));
                if (attr == null)
                {
                    throw new InvalidOperationException("Found registration without Autowire attribute" + registration);
                }
                if (attr.Precedence <= this.precedence && attr.Precedence > foundPrecedence)
                {
                    foundRegistration = registration;
                    foundPrecedence = attr.Precedence;
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
            get { return true; }
        }
    }
}