namespace Kheper.Core.Dependency
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Ninject;
    using Ninject.Activation;
    using Ninject.Activation.Caching;
    using Ninject.Components;
    using Ninject.Extensions.Conventions;
    using Ninject.Infrastructure;
    using Ninject.Parameters;
    using Ninject.Planning;
    using Ninject.Planning.Bindings;
    using Ninject.Planning.Bindings.Resolvers;
    using Ninject.Syntax;

    /// <summary>
    /// <para>
    /// Ninject extention for <see cref="IMissingBindingResolver" /> role.
    /// Allows to substitute components depending on the execution environment - production, development, QA.
    /// </para>
    /// <para>
    /// It allows to bound multiple components to one service.
    /// When the service is requested it selects a component basing on preemptive algorithm.
    /// The component with higher precedence which does not exceed defined threshold will be chosen.
    /// </para>
    /// TODO: extract responsibility of configuring internal container to follow SRP.
    /// </summary>
    public class PreemptiveAutowiring : NinjectComponent, IMissingBindingResolver
    {
        /// <summary>
        /// Internal registry used for consulting purposes only.
        /// </summary>
        private readonly StandardKernel kernel;

        /// <summary>
        /// Effective level of precedence.
        /// </summary>
        private readonly EAutowiringPrecedence precedence;

        public PreemptiveAutowiring()
        {
            var precedenceText = ConfigurationManager.AppSettings["AutowiringPrecedence"];
            if (!Enum.TryParse(precedenceText, out this.precedence))
            {
                if (!string.IsNullOrEmpty(precedenceText))
                {
                    throw new InvalidOperationException("Unknown autowiring precedence: " + precedenceText);
                }
                this.precedence = EAutowiringPrecedence.Application;
            }

            this.kernel = new StandardKernel();
            this.kernel.Bind(a => a
                .FromThisAssembly()
                .SelectAllClasses()
                .WithAttribute<ComponentAttribute>()
                .BindAllInterfaces()
                .Configure(this.IdentifyScope));
        }

        public void IdentifyScope(IBindingWhenInNamedWithOrOnSyntax<object> syntax, Type serviceType)
        {
            var attribute = (LifetimeScopeAttribute)Attribute.GetCustomAttribute(serviceType, typeof(LifetimeScopeAttribute));
            if (attribute != null)
            {
                var scope = attribute.Scope;
                switch (scope)
                {
                    case ELifetimeScope.Singleton:
                        syntax.InSingletonScope();
                        break;
                    case ELifetimeScope.Transient:
                        syntax.InTransientScope();
                        break;
                    case ELifetimeScope.Thread:
                        syntax.InThreadScope();
                        break;
                    case ELifetimeScope.Request:
                        throw new NotImplementedException("TODO: Invoke InRequestScope() via reflection");
#pragma warning disable 162
                        break;
#pragma warning restore 162
                    case ELifetimeScope.Unspecified:
                        // decision will be made by the container
                        break;
                    default:
                        throw new NotImplementedException("Lifetime scope " + scope + " is not recognized");
                }
            }
        }

        public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> incomeBindings, IRequest request)
        {
            IEnumerable<IBinding> bindings = this.kernel.GetBindings(request.Service);

            IBinding foundRegistration = null;
            var foundPrecedence = EAutowiringPrecedence.Unspecified;

            foreach (var binding in bindings)
            {
                Type type = GetBoundToType(this.kernel, binding, binding.Service);
                var attribute = (ComponentAttribute)Attribute.GetCustomAttribute(type, typeof(ComponentAttribute));
                if (attribute == null)
                {
                    throw new InvalidOperationException("Found registration without Autowire attribute" + binding);
                }

                EAutowiringPrecedence attributePrecedence = attribute.Precedence != EAutowiringPrecedence.Unspecified ?
                    attribute.Precedence : EAutowiringPrecedence.Application;

                // if registered component has precedence which is enabled for the current environement
                // and registered component has greater precedence over the other components
                // then we use this component in resolution
                if (attributePrecedence <= this.precedence && attributePrecedence > foundPrecedence)
                {
                    foundRegistration = binding;
                    foundPrecedence = attribute.Precedence;
                }
            }

            if (foundRegistration != null)
            {
                return new[] { foundRegistration };
            }
            else
            {
                return Enumerable.Empty<IBinding>();
            }
        }

        // this one is from StackOverflow.com
        private static Type GetBoundToType(IKernel kernel, IBinding binding, Type boundType)
        {
            if (binding != null)
            {
                if (binding.Target != BindingTarget.Type && binding.Target != BindingTarget.Self)
                {
                    // TODO: maybe the code  below could work for other BindingTarget values, too, feelfree to try
                    throw new InvalidOperationException(string.Format("Cannot find the type to which {0} is bound to, because it is bound using a method, provider or constant ", boundType));
                }

                var req = kernel.CreateRequest(boundType, metadata => true, new IParameter[0], true, false);
                var cache = kernel.Components.Get<ICache>();
                var planner = kernel.Components.Get<IPlanner>();
                var pipeline = kernel.Components.Get<IPipeline>();
                var provider = binding.GetProvider(new Context(kernel, req, binding, cache, planner, pipeline));
                return provider.Type;
            }

            if (boundType.IsClass && !boundType.IsAbstract)
            {
                return boundType;
            }
            throw new InvalidOperationException(string.Format("Cannot find the type to which {0} is bound to", boundType));
        }

        public static void Extend(IKernel kernel)
        {
            kernel.Components.Add<IMissingBindingResolver, PreemptiveAutowiring>();
        }
    }
}